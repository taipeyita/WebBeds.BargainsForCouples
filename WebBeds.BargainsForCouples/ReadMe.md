#Developer Coding Exercise

I have completed the developer coding test. Based on your feedback from my previous solution, i 
have decided to restructure my solution to closely resemble the requirements of the test. Apologies if my previous solution 
was not clear.

I did not have the secret code for the bargains for couples api, so you would need to add the secret code 
for the application to work.

There are four projects in the solution.

1. BargainsForCouples.MicroService : Api Project. Please amend the appsettings.json file to add your secret code for 
  the bargains for more api client.

2. docker-compose: I have added docker support for the application, so to run the application you would need to run 
docker-compose as the startup project. This project relies on redis for the caching functionality. This is automatically handled 
if you run the application using docker-compose.

3.WebBeds.BargainsForCouples.Core: Class library that contains helper class for the Final price calculation.

4. WebBeds.BargainsForCouples.UnitTest: xUnit unit test project for testing the final price calculation.


#Once docker is running here is an example of the url to be used 
{Scheme}://{ServiceHost}:{ServicePort}/api/Hotel/GetBargain?destinationId=12345&nights=9