# Release Retention solution

## General Assumptions
- The solution will simulate an integration API, pulling the data from an external source and doing the computation there
- Because the solution is more like integrating with another endpoint where the data has to be loaded into the application, it will make use of IEnumerable instead of IQueryable, as IEnumerable is more suitable for the scenario where the data has already been loaded to the memory
- The data will be stored in json files. The structure of the json files is similar to the sample files provided, meaning they will always be lists of items
- There is one Deployment per environment, as per the sample data and the definition deployment below
- A release must belong to one project and must specify the projectId 
- A deployment must specify an environment Id
- If we have a release that is deployed to different environments, there could be a situation where that release may not be selected for this environment, but will be selected again for another environment
- An example, 3 release A, B, C from the same project. A and B are deployed to Staging, while C is deployed to Staging and Production. If the threshold is 2, A, B, and C will all be selected
- The reason is A and B are selected because they are the two recent releases to Staging. C is selected because it is the recent release to Production

## Solution walkthrough

### General
In general, the solution will
- Break down the tasks involved into services
- Have interfaces and default concrete service implememtations, and the service will depend on each other by interfaces instead of the default concrete implementation so we can swap between different implementations easier (e.g swapping the real implementation with a mocked one when unit testing), and enable integrating with Dependency Injection later 
- Load data from the sample json files 
- Join the data based on the ids
- Group the data based on the environments. In each group, the data is grouped again based on the projects, reordered by the deployment date, and taken based on the input number of releases to keep

### Detailed Breakdown

Here is the breakdown of the services and what they do
#### DefaultDataLoader.cs
- Implements IDataLoader
- Loads the data from a json file into an **IEnumerable** list. The reason to load it as a list is because the structure of the json file is a list. Also, using IEnumerable has the advantage of potentially eliminate multiple iterations through the entire list
- Uses **StreamReader** to increase the performance in the scenario where we need to load big json files
- If an exception is thrown, the function simply returns an empty collection
- There are two test cases for this class. They are inside DefaultDataLoaderTests

#### DefaultReleaseDataLoader.cs
- Implements IReleaseDataLoader
- Depends on IDataLoader
- Provides methods to load deployment, environment, release, and project data, which are essentially a wrapper around the LoadCollectionAsync method
- No test for this as they are just wrappers around the method that has already been tested

#### DefaultReleaseDataProcessor.cs
- Implements IReleaseDataProcessor
- Depends on IReleaseDataLoader
- Loads the deployments, environments, projects, and releases with the help of the methods in IReleaseDataLoader, then joins the data together by ids using **LINQ Join**
- One test is created to test the joining

#### DefaultReleaseProcessor.cs
- Implements IReleaseProcessor
- Provides a method that takes in one parameter that is the number of releases to keep
- Sorts the flattened/joined release data by environments and projects, orders the release by deployment date, and keeps the releases based on the release to keep parameters with the use of **LINQ OrderBy**
- Returns a list of releases to keep. Each will have the following information: Release, Project, Environment, and Index
- 4 test cases for this class, with 3 being the tests mentioned in the StartHere.md

#### DefaultReleaseDataFormatter.cs
- Implements IReleaseDataFormatter
- Outputs the log string for the reason why the releases are kept
- Uses **StringBuilder** to reduce the memory consumption when dealing with string
- There are 3 test cases for this class. The format is of the following/
Release Release-2-3 of project Pet Shop was kept because it was the 2nd most recent release to Production

## Limitations
- Not accounting for the situation where the data could be orphan (e.g The project or the environment is deleted at some point)
- The processing method is a bit computational expensive as it makes use of nested loops
- The joining of the data can also potentially be expensive too
- If the source of the data is a database then the use of IEnumerable might incur in some extra memory consumption, as it means we have to load everything into the memory to begin processing. **We should replace it with IQueryable if we integrate this app with a database system**, as IQueryable only loads the data on demand

## The use of AI in this solution
- I used cursor to help me generate the test cases
- I provided cursor the Deployments.json file and the class Deployment.cs. This is the command that I use 
> Could you help me create some data for testing purposes?
I would like for you to output a variable called x of the type List<Deployment>. The data to it is from the json file I included
- For the retention logic, I built it myself, which is why there are rooms for improvements and things to optimize haha :)
- I find that AI are super helpful in terms of helping me come up with tests. The key thing is to be super specific about what you want the AI to do. AI is really good when you provide detailed data source and the mapping that you expect