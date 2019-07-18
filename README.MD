# Dependency Issues
-     <PackageReference Include="RavenDB.Client" Version="4.0.0-rc-40023" />
  If this is update, starting the DB code will have to be modified for anything that references Operations.Admin, anything outside this RC file
  Its been deprecated: https://www.nuget.org/packages/RavenDB.Client/4.0.0-rc-40023
  
  It will be best to update the code for this
  
 -