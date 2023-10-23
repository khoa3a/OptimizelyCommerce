https://docs.developers.optimizely.com/customized-commerce/docs/creating-your-project

dotnet-episerver create-cms-database EmptyCommerce1.csproj -S . -E
dotnet-episerver create-commerce-database EmptyCommerce1.csproj -S . -E --reuse-cms-user
dotnet-episerver add-admin-user EmptyCommerce1.csproj -u khoa-nguyen -p Aa123456@ -e khoa.nguyen@niteco.com -c EcfSqlConnection

dotnet-episerver create-cms-database EmptyCommerce2.csproj -S . -E -dn EmptyCommerce2_CMS
dotnet-episerver create-commerce-database EmptyCommerce2.csproj -S . -E -dn EmptyCommerce2_Commerce --reuse-cms-user
dotnet-episerver add-admin-user EmptyCommerce2.csproj -u khoa-nguyen -p Aa123456@ -e khoa.nguyen@niteco.se -c EcfSqlConnection

https://world.optimizely.com/forum/developer-forum/Commerce/Thread-Container/2018/7/getting-error-when-loading-commerce-catalog/
