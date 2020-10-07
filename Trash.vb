Module Trash


    'using Acme;
    'using System;
    'using System.IO;
    'using System.Web.Services;
    'using System.Data;
    'using System.Data.SqlClient;

    'using Microsoft.Samples.Windows.Forms.Cs.MasterDetails.Data;

    'namespace Microsoft.Samples.Windows.Forms.Cs.MasterDetails
    '{
    '    public class CustomersAndOrders : System.Web.Services.WebService
    '    {

    '        //Retrieve Customers and Orders
    '        [ WebMethod ]
    '        public DataSet GetCustomersAndOrders()
    '        {

    '            CustomersAndOrdersDataSet customersAndOrdersDataSet1 = 
    '                new CustomersAndOrdersDataSet();
    '            SqlConnection con = new SqlConnection(
    '                "server=(local)\\NetSDK;database=northwind;Integrated Security=SSPI");
    '            SqlDataAdapter cmdCustomers = new SqlDataAdapter(
    '                "Select * from Customers", con);
    '            SqlDataAdapter cmdOrders = new SqlDataAdapter("Select * from Orders", con);

    '        try {
    '                cmdCustomers.Fill(customersAndOrdersDataSet1, "Customers");
    '                cmdOrders.Fill(customersAndOrdersDataSet1, "Orders");

    '                return customersAndOrdersDataSet1;
    '        }
    '        catch (Exception ex){
    '                throw (ex);
    '        }
    '        finally{
    '            con.Close();
    '        }
    '        }

    '        //Push Customers and Orders changes back into the database
    '        [ WebMethod ]
    '        public DataSet UpdateCustomersAndOrders(DataSet ds)
    '        {
    '            SqlConnection con = new SqlConnection(
    '                "server=(local)\\NetSDK;database=northwind;Integrated Security=SSPI");

    '            //We want the update as part of a single transaction so open the connection
    '            //first and pass it to the updates
    '            con.Open();
    '            SqlTransaction tran = con.BeginTransaction();
    '            try
    '            {
    '                UpdateCustomers(ds, con, tran);
    '                UpdateOrders(ds, con, tran);

    '                //Don't commit if errors occurred
    '                if (!ds.HasErrors)
    '                {
    '                    //Commit the txn
    '                    tran.Commit();

    '                    //Commit the changes to the dataset
    '                    ds.AcceptChanges();
    '                }
    '                else
    '                {
    '                    //Dataset has errors - roll back the transaction
    '                    tran.Rollback();
    '                }
    '            }
    '            catch(Exception e)
    '            {
    '                //Roll back the txn if we have a failure
    '                tran.Rollback();
    '                throw e;
    '            }
    '            finally
    '            {
    '                //Make sure we close the connection no matter what
    '                con.Close();
    '            }

    '            return ds ;
    '        }

    '        //Update Customers
    '        private void UpdateCustomers(DataSet ds, SqlConnection con, SqlTransaction tran)
    '        {
    '            // Note:  You must have a CustomerID and Company Name
    '            const string insertCustSQL = "INSERT INTO [Customers]([CustomerID], " +
    '                "[CompanyName], [ContactName], [ContactTitle], [Address], [City], " +
    '                "[Region], [PostalCode], [Country], [Phone], [Fax]) VALUES " +
    '                "(@CustomerID, @CompanyName, @ContactName, @ContactTitle, @Address, " +
    '                "@City, @Region, @PostalCode, @Country, @Phone, @Fax)";

    '            // Note:  The CustomerID cannot be changed unless there is no
    '            //        Orders associated with it
    '            const string updateCustSQL = "UPDATE [Customers] SET [CustomerID] = " +
    '                "@CustomerID, [CompanyName] = @CompanyName, [ContactName] = " +
    '                "@ContactName, [ContactTitle] = @ContactTitle, [Address] = @Address, " +
    '                "[City] = @City, [Region] = @Region, [PostalCode] = @PostalCode, " +
    '                "[Country] = @Country, [Phone] = @Phone, [Fax] = @Fax " +
    '                "WHERE [CustomerID] = @oldCustomerID ";

    '            // Note:  Delete will only succeed if the Customer does not have any
    '            //        Orders associated with it
    '            const string deleteCustSQL = "DELETE FROM [Customers] WHERE " +
    '                "[CustomerID] = @CustomerID ";

    '            //Create the command
    '            SqlDataAdapter cmdCustomers = 
    '                new SqlDataAdapter("Select * from Customers", con);

    '            //Add the insert, update and delete commands
    '            cmdCustomers.InsertCommand = new SqlCommand(insertCustSQL, con, tran);
    '            buildCustomersInsertParams(cmdCustomers.InsertCommand);
    '            cmdCustomers.UpdateCommand = new SqlCommand(updateCustSQL, con, tran);
    '            buildCustomersUpdateParams(cmdCustomers.UpdateCommand);
    '            cmdCustomers.DeleteCommand = new SqlCommand(deleteCustSQL, con, tran);
    '            buildCustomersDeleteParams(cmdCustomers.DeleteCommand);

    '            //Catch the row update commands so that we can deal with errors
    '            cmdCustomers.RowUpdated += new SqlRowUpdatedEventHandler(
    '                this.CustomersAndOrders_RowUpdated);

    '            foreach (DataRow row in ds.Tables["Customers"].Rows)
    '            {
    '                string[] colNames = new string[] {
    '                    "CustomerID", "CompanyName", "ContactName", "ContactTitle",
    '                    "Address", "City", "Region", "PostalCode", "Country",
    '                    "Phone", "Fax"
    '                };

    '                foreach(string col in colNames)
    '                {
    '                    if (!row.IsNull(col))
    '                    {
    '                        if ( !InputValidator.IsSafeText(row[col].ToString()) )
    '                        {
    '                            string msg = col + " can only contain limited punctuation\r\n";
    '                            row.RowError += msg;
    '                            row.SetColumnError(col, msg);
    '                        }
    '                    }
    '                }
    '            }

    '            //Apply the updates
    '            if ( !ds.HasErrors )
    '                cmdCustomers.Update(ds, "Customers");
    '        }

    '        //Update Orders
    '        private void UpdateOrders(DataSet ds, SqlConnection con, SqlTransaction tran)
    '        {
    '            const string insertOrderSQL = "INSERT INTO [Orders](" +
    '                "[CustomerID], [EmployeeID], [OrderDate], [RequiredDate], " +
    '                "[ShippedDate], [ShipVia], [Freight], [ShipName], [ShipAddress], " +
    '                "[ShipCity], [ShipRegion], [ShipPostalCode], [ShipCountry]) " +
    '                "VALUES (@CustomerID, @EmployeeID, @OrderDate, " +
    '                "@RequiredDate, @ShippedDate, @ShipVia, @Freight, @ShipName, " +
    '                "@ShipAddress, @ShipCity, @ShipRegion, @ShipPostalCode, @ShipCountry)";

    '            const string updateOrderSQL = "UPDATE [Orders] SET [CustomerID] = " +
    '                "@CustomerID, [EmployeeID] = @EmployeeID, [OrderDate] = @OrderDate, " +
    '                "[RequiredDate] = @RequiredDate, [ShippedDate] = @ShippedDate, " +
    '                "[ShipVia] = @ShipVia, [Freight] = @Freight, [ShipName] = @ShipName, " +
    '                "[ShipAddress] = @ShipAddress, [ShipCity] = @ShipCity, " +
    '                "[ShipRegion] = @ShipRegion, [ShipPostalCode] = @ShipPostalCode, " +
    '                "[ShipCountry] = @ShipCountry  WHERE [OrderID] = @oldOrderID";

    '            // Delete will only work if there are no order details associated with
    '            // the order number
    '            const string deleteOrderSQL = "DELETE FROM [Orders] WHERE " +
    '                "[OrderID] = @OrderID ";

    '            //Create the command
    '            SqlDataAdapter cmdOrders = new SqlDataAdapter("Select * from Orders", con);

    '            //Add the insert, update and delete commands
    '            cmdOrders.InsertCommand = new SqlCommand(insertOrderSQL, con, tran);
    '            buildOrdersInsertParams(cmdOrders.InsertCommand);
    '            cmdOrders.UpdateCommand = new SqlCommand(updateOrderSQL, con, tran);
    '            buildOrdersUpdateParams(cmdOrders.UpdateCommand);
    '            cmdOrders.DeleteCommand = new SqlCommand(deleteOrderSQL, con, tran);
    '            buildOrdersDeleteParams(cmdOrders.DeleteCommand);

    '            //Catch the row update commands so that we can deal with errors
    '            cmdOrders.RowUpdated += new SqlRowUpdatedEventHandler(
    '                this.CustomersAndOrders_RowUpdated);

    '            foreach (DataRow row in ds.Tables["Orders"].Rows)
    '            {
    '                string[] colNames = new string[] {
    '                    "CustomerID", "EmployeeID", "OrderDate", "RequiredDate",
    '                    "ShippedDate", "ShipVia", "Freight", "ShipName", "ShipAddress",
    '                    "ShipCity", "ShipRegion", "ShipPostalCode", "ShipCountry"
    '                };

    '                foreach(string col in colNames)
    '                {
    '                    if (!row.IsNull(col))
    '                    {
    '                        if ( !InputValidator.IsSafeText(row[col].ToString()) )
    '                        {
    '                            string msg = col + " can only contain limited punctuation\r\n";
    '                            row.RowError += msg;
    '                            row.SetColumnError(col, msg);
    '                        }
    '                    }
    '                }
    '            }

    '            //Apply the updates
    '            if ( !ds.HasErrors )
    '                cmdOrders.Update(ds, "Orders");
    '        }

    '        //Once the row update has been pushed back into the database look for errors
    '        private void CustomersAndOrders_RowUpdated(
    '            object sender, SqlRowUpdatedEventArgs rue)
    '        {
    '            if (rue.Status == UpdateStatus.ErrorsOccurred)
    '            {
    '                rue.Status = UpdateStatus.Continue;
    '                rue.Row.RowError = rue.Errors.Message + 
    '                    " - Deliberate error see WebService for details";
    '            }
    '            else
    '            {
    '                rue.Row.ClearErrors();
    '            }
    '        }

    '        private void buildCustomersUpdateParams(SqlCommand workCommand)
    '        {
    '            SqlParameter workParam = null;
    '            workParam = workCommand.Parameters.Add(new SqlParameter(
    '                "@CustomerID", SqlDbType.NChar, 5));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "CustomerID";
    '            workParam = workCommand.Parameters.Add(new SqlParameter(
    '                "@CompanyName", SqlDbType.NVarChar, 40));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "CompanyName";
    '            workParam = workCommand.Parameters.Add(new SqlParameter(
    '                "@ContactName", SqlDbType.NVarChar, 30));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "ContactName";
    '            workParam = workCommand.Parameters.Add(new SqlParameter(
    '                "@ContactTitle", SqlDbType.NVarChar, 30));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "ContactTitle";
    '            workParam = workCommand.Parameters.Add(new SqlParameter(
    '                "@Address", SqlDbType.NVarChar, 60));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "Address";
    '            workParam = workCommand.Parameters.Add(new SqlParameter(
    '                "@City", SqlDbType.NVarChar, 15));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "City";
    '            workParam = workCommand.Parameters.Add(new SqlParameter(
    '                "@Region", SqlDbType.NVarChar, 15));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "Region";
    '            workParam = workCommand.Parameters.Add(new SqlParameter(
    '                "@PostalCode", SqlDbType.NVarChar, 10));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "PostalCode";
    '            workParam = workCommand.Parameters.Add(new SqlParameter(
    '                "@Country", SqlDbType.NVarChar, 15));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "Country";
    '            workParam = workCommand.Parameters.Add(new SqlParameter(
    '                "@Phone", SqlDbType.NVarChar, 24));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "Phone";
    '            workParam = workCommand.Parameters.Add(new SqlParameter(
    '                "@Fax", SqlDbType.NVarChar, 24));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "Fax";
    '            workParam = workCommand.Parameters.Add(new SqlParameter(
    '                "@oldCustomerID", SqlDbType.NChar, 5));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "CustomerID";
    '            workParam.SourceVersion = DataRowVersion.Original;
    '        }

    '        private void buildCustomersInsertParams(SqlCommand workCommand)
    '        {
    '            SqlParameter workParam = null;
    '            workParam = workCommand.Parameters.Add(new SqlParameter(
    '                "@CustomerID", SqlDbType.NChar, 5));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "CustomerID";
    '            workParam = workCommand.Parameters.Add(new SqlParameter(
    '                "@CompanyName", SqlDbType.NVarChar, 40));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "CompanyName";
    '            workParam = workCommand.Parameters.Add(new SqlParameter(
    '                "@ContactName", SqlDbType.NVarChar, 30));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "ContactName";
    '            workParam = workCommand.Parameters.Add(new SqlParameter(
    '                "@ContactTitle", SqlDbType.NVarChar, 30));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "ContactTitle";
    '            workParam = workCommand.Parameters.Add(new SqlParameter(
    '                "@Address", SqlDbType.NVarChar, 60));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "Address";
    '            workParam = workCommand.Parameters.Add(new SqlParameter(
    '                "@City", SqlDbType.NVarChar, 15));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "City";
    '            workParam = workCommand.Parameters.Add(new SqlParameter(
    '                "@Region", SqlDbType.NVarChar, 15));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "Region";
    '            workParam = workCommand.Parameters.Add(new SqlParameter(
    '                "@PostalCode", SqlDbType.NVarChar, 10));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "PostalCode";
    '            workParam = workCommand.Parameters.Add(new SqlParameter(
    '                "@Country", SqlDbType.NVarChar, 15));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "Country";
    '            workParam = workCommand.Parameters.Add(new SqlParameter(
    '                "@Phone", SqlDbType.NVarChar, 24));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "Phone";
    '            workParam = workCommand.Parameters.Add(new SqlParameter(
    '                "@Fax", SqlDbType.NVarChar, 24));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "Fax";
    '        }

    '        private void buildCustomersDeleteParams(SqlCommand workCommand)
    '        {
    '            SqlParameter workParam = null;
    '            workParam = workCommand.Parameters.Add(new SqlParameter(
    '                "@CustomerID", SqlDbType.NChar, 5));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "CustomerID";
    '            workParam.SourceVersion = DataRowVersion.Original;
    '        }

    '        private void buildOrdersUpdateParams(SqlCommand workCommand)
    '        {
    '            SqlParameter workParam = null;
    '            workParam = workCommand.Parameters.Add(new SqlParameter(
    '                "@OrderID", SqlDbType.Int, 4));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "OrderID";
    '            workParam = workCommand.Parameters.Add(new SqlParameter(
    '                "@CustomerID", SqlDbType.NChar, 5));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "CustomerID";
    '            workParam = workCommand.Parameters.Add(new SqlParameter(
    '                "@EmployeeID", SqlDbType.Int, 4));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "EmployeeID";
    '            workParam = workCommand.Parameters.Add(new SqlParameter(
    '                "@OrderDate", SqlDbType.DateTime, 8));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "OrderDate";
    '            workParam = workCommand.Parameters.Add(new SqlParameter(
    '                "@RequiredDate", SqlDbType.DateTime, 8));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "RequiredDate";
    '            workParam = workCommand.Parameters.Add(new SqlParameter(
    '                "@ShippedDate", SqlDbType.DateTime, 8));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "ShippedDate";
    '            workParam = workCommand.Parameters.Add(new SqlParameter(
    '                "@ShipVia", SqlDbType.Int, 4));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "ShipVia";
    '            workParam = workCommand.Parameters.Add(new SqlParameter(
    '                "@Freight", SqlDbType.Money, 8));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "Freight";
    '            workParam = workCommand.Parameters.Add(new SqlParameter(
    '                "@ShipName", SqlDbType.NVarChar, 40));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "ShipName";
    '            workParam = workCommand.Parameters.Add(new SqlParameter(
    '                "@ShipAddress", SqlDbType.NVarChar, 60));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "ShipAddress";
    '            workParam = workCommand.Parameters.Add(new SqlParameter(
    '                "@ShipCity", SqlDbType.NVarChar, 15));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "ShipCity";
    '            workParam = workCommand.Parameters.Add(new SqlParameter(
    '                "@ShipRegion", SqlDbType.NVarChar, 15));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "ShipRegion";
    '            workParam = workCommand.Parameters.Add(new SqlParameter(
    '                "@ShipPostalCode", SqlDbType.NVarChar, 10));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "ShipPostalCode";
    '            workParam = workCommand.Parameters.Add(new SqlParameter(
    '                "@ShipCountry", SqlDbType.NVarChar, 15));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "ShipCountry";
    '            workParam = workCommand.Parameters.Add(new SqlParameter(
    '                "@oldOrderID", SqlDbType.Int, 4));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "OrderID";
    '            workParam.SourceVersion = DataRowVersion.Original;
    '        }

    '        private void buildOrdersInsertParams(SqlCommand workCommand)
    '        {
    '            SqlParameter workParam = null;
    '            workParam = workCommand.Parameters.Add(new SqlParameter(
    '                "@CustomerID", SqlDbType.NChar, 5));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "CustomerID";
    '            workParam = workCommand.Parameters.Add(new SqlParameter(
    '                "@EmployeeID", SqlDbType.Int, 4));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "EmployeeID";
    '            workParam = workCommand.Parameters.Add(new SqlParameter(
    '                "@OrderDate", SqlDbType.DateTime, 8));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "OrderDate";
    '            workParam = workCommand.Parameters.Add(new SqlParameter(
    '                "@RequiredDate", SqlDbType.DateTime, 8));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "RequiredDate";
    '            workParam = workCommand.Parameters.Add(new SqlParameter(
    '                "@ShippedDate", SqlDbType.DateTime, 8));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "ShippedDate";
    '            workParam = workCommand.Parameters.Add(new SqlParameter(
    '                "@ShipVia", SqlDbType.Int, 4));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "ShipVia";
    '            workParam = workCommand.Parameters.Add(new SqlParameter(
    '                "@Freight", SqlDbType.Money, 8));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "Freight";
    '            workParam = workCommand.Parameters.Add(new SqlParameter(
    '                "@ShipName", SqlDbType.NVarChar, 40));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "ShipName";
    '            workParam = workCommand.Parameters.Add(new SqlParameter(
    '                "@ShipAddress", SqlDbType.NVarChar, 60));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "ShipAddress";
    '            workParam = workCommand.Parameters.Add(new SqlParameter(
    '                "@ShipCity", SqlDbType.NVarChar, 15));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "ShipCity";
    '            workParam = workCommand.Parameters.Add(new SqlParameter(
    '                "@ShipRegion", SqlDbType.NVarChar, 15));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "ShipRegion";
    '            workParam = workCommand.Parameters.Add(new SqlParameter(
    '                "@ShipPostalCode", SqlDbType.NVarChar, 10));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "ShipPostalCode";
    '            workParam = workCommand.Parameters.Add(new SqlParameter(
    '                "@ShipCountry", SqlDbType.NVarChar, 15));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "ShipCountry";
    '        }

    '        private void buildOrdersDeleteParams(SqlCommand workCommand)
    '        {
    '            SqlParameter workParam = null;
    '            workParam = workCommand.Parameters.Add(new SqlParameter(
    '                "@OrderID", SqlDbType.Int, 4));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "OrderID";
    '            workParam.SourceVersion = DataRowVersion.Original;
    '        }
    '    }
    '}



    'namespace Microsoft.Samples.Windows.Forms.Cs.Update
    '{
    '	using Acme;
    '    using System;
    '    using System.IO;
    '    using System.Web.Services;
    '    using System.Data;
    '    using System.Data.SqlClient;

    '    public class UpdateCustomers : System.Web.Services.WebService
    '    {


    '        //Push Customers changes back into the database
    '        [ WebMethod ]
    '        public DataSet Save(DataSet customersDataSet)
    '        {

    '            SqlConnection con = new SqlConnection("server=(local)\\NetSDK;database=northwind;Integrated Security=SSPI");

    '            //We want the update as part of a single transaction so open the connection
    '            //first and pass it to the updates
    '            con.Open();
    '            SqlTransaction tran = con.BeginTransaction();
    '            try
    '            {

    '                ApplyCustomerUpdates(customersDataSet, con, tran);

    '                //Don't commit if errors occured
    '                if (!customersDataSet.HasErrors)
    '                {
    '                    //Commit the txn
    '                    tran.Commit();

    '                    //Commit the changes to the dataset
    '                    customersDataSet.AcceptChanges();

    '                }
    '                else
    '                {

    '                    //Dataset has errors - roll back the transaction
    '                    tran.Rollback();
    '                }

    '            }
    '            catch(Exception e)
    '            {

    '                //Roll back the txn if we have a failure
    '                tran.Rollback();
    '                throw e;

    '            }
    '            finally
    '            {

    '                //Make sure we close the connection no matter what
    '                con.Close();
    '            }

    '            return customersDataSet ;
    '        }

    '        //Update Customers
    '        private void ApplyCustomerUpdates(DataSet ds, SqlConnection con, SqlTransaction tran)
    '        {

    '            const string insertCustSQL = "INSERT INTO [Customers]([CustomerID], [CompanyName], [ContactName], [ContactTitle], [Address], [City], [Region], [PostalCode], [Country], [Phone], [Fax]) VALUES (@CustomerID, @CompanyName, @ContactName, @ContactTitle, @Address, @City, @Region, @PostalCode, @Country, @Phone, @Fax)";
    '            const string updateCustSQL = "UPDATE [Customers] SET [CustomerID] = @CustomerID, [CompanyName] = @CompanyName, [ContactName] = @ContactName, [ContactTitle] = @ContactTitle, [Address] = @Address, [City] = @City, [Region] = @Region, [PostalCode] = @PostalCode, [Country] = @Country, [Phone] = @Phone, [Fax] = @Fax WHERE [CustomerID] = @oldCustomerID ";
    '            const string deleteCustSQL = "DELETE FROM [Customers] WHERE [CustomerID] = @CustomerID ";

    '            //Create the command
    '            SqlDataAdapter cmdCustomers = new SqlDataAdapter("Select * from Customers", con);

    '            //Add the insert, update and delete commands
    '            cmdCustomers.InsertCommand = new SqlCommand(insertCustSQL, con);
    '            cmdCustomers.InsertCommand.Transaction = tran;
    '            buildCustomersInsertParams(cmdCustomers.InsertCommand);
    '            cmdCustomers.UpdateCommand = new SqlCommand(updateCustSQL, con);
    '            cmdCustomers.UpdateCommand.Transaction = tran;
    '            buildCustomersUpdateParams(cmdCustomers.UpdateCommand);
    '            cmdCustomers.DeleteCommand = new SqlCommand(deleteCustSQL, con);
    '            cmdCustomers.DeleteCommand.Transaction = tran;
    '            buildCustomersDeleteParams(cmdCustomers.DeleteCommand);

    '            //Catch the row update commands so that we can deal with errors
    '            cmdCustomers.RowUpdated += new SqlRowUpdatedEventHandler(this.Customers_RowUpdated);

    '            foreach (DataRow row in ds.Tables["Customers"].Rows)
    '            {
    '                string[] colNames = new string[] {"CustomerID", "CompanyName", "ContactName", "ContactTitle", "Address", "City", "Region", "Country", "Phone", "Fax"};
    '                foreach(string col in colNames)
    '                {
    '                    if (!row.IsNull(col))
    '                    {
    '                        if ( !InputValidator.IsSafeText(row[col].ToString()) )
    '                        {
    '                            string msg = col + " can only contain limited punctuation\r\n";
    '                            row.RowError += msg;
    '                            row.SetColumnError(col, msg);
    '                        }
    '                    }
    '                }

    '                //Check for a bad post code - for US post code must be non-null & numeric
    '                if (row["Country"].Equals("USA"))
    '                {
    '                    if (row.IsNull("PostalCode"))
    '                    {
    '                        row.RowError += "Zip Code Required\r\n";
    '                        row["PostalCode"] = "Zip Code required";
    '                        row.SetColumnError("PostalCode", "Zip Code required");
    '                    }
    '                    else
    '                    {
    '                        //Attempt to convert the zip code to a number
    '                        try
    '                        {
    '                            Convert.ToInt32(row["PostalCode"]);
    '                        }
    '                        catch(Exception)
    '                        {
    '                            row.RowError += "Zip Code is invalid\r\n";
    '                            row.SetColumnError("PostalCode", "Invalid Zip Code");
    '                        }
    '                    }
    '                }
    '            }

    '            //Apply the updates
    '            if (!ds.HasErrors)
    '                cmdCustomers.Update(ds, "Customers");

    '        }

    '        //Once the row update has been pushed back into the database look for errors
    '        private void Customers_RowUpdated(object sender, SqlRowUpdatedEventArgs rue)
    '        {

    '            if (rue.Status == UpdateStatus.ErrorsOccurred)
    '            {
    '                rue.Status = UpdateStatus.Continue;
    '                rue.Row.RowError = rue.Errors.Message ;
    '            }
    '            else
    '            {
    '                rue.Row.ClearErrors();
    '            }
    '        }

    '        private void buildCustomersUpdateParams(SqlCommand workCommand)
    '        {

    '            SqlParameter workParam = null;
    '            workParam = workCommand.Parameters.Add(new SqlParameter("@CustomerID", SqlDbType.NChar, 5));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "CustomerID";
    '            workParam = workCommand.Parameters.Add(new SqlParameter("@CompanyName", SqlDbType.NVarChar, 40));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "CompanyName";
    '            workParam = workCommand.Parameters.Add(new SqlParameter("@ContactName", SqlDbType.NVarChar, 30));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "ContactName";
    '            workParam = workCommand.Parameters.Add(new SqlParameter("@ContactTitle", SqlDbType.NVarChar, 30));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "ContactTitle";
    '            workParam = workCommand.Parameters.Add(new SqlParameter("@Address", SqlDbType.NVarChar, 60));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "Address";
    '            workParam = workCommand.Parameters.Add(new SqlParameter("@City", SqlDbType.NVarChar, 15));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "City";
    '            workParam = workCommand.Parameters.Add(new SqlParameter("@Region", SqlDbType.NVarChar, 15));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "Region";
    '            workParam = workCommand.Parameters.Add(new SqlParameter("@PostalCode", SqlDbType.NVarChar, 10));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "PostalCode";
    '            workParam = workCommand.Parameters.Add(new SqlParameter("@Country", SqlDbType.NVarChar, 15));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "Country";
    '            workParam = workCommand.Parameters.Add(new SqlParameter("@Phone", SqlDbType.NVarChar, 24));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "Phone";
    '            workParam = workCommand.Parameters.Add(new SqlParameter("@Fax", SqlDbType.NVarChar, 24));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "Fax";
    '            workParam = workCommand.Parameters.Add(new SqlParameter("@oldCustomerID", SqlDbType.NChar, 5));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "CustomerID";
    '        }

    '        private void buildCustomersInsertParams(SqlCommand workCommand)
    '        {

    '            SqlParameter workParam = null;
    '            workParam = workCommand.Parameters.Add(new SqlParameter("@CustomerID", SqlDbType.NChar, 5));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "CustomerID";
    '            workParam = workCommand.Parameters.Add(new SqlParameter("@CompanyName", SqlDbType.NVarChar, 40));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "CompanyName";
    '            workParam = workCommand.Parameters.Add(new SqlParameter("@ContactName", SqlDbType.NVarChar, 30));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "ContactName";
    '            workParam = workCommand.Parameters.Add(new SqlParameter("@ContactTitle", SqlDbType.NVarChar, 30));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "ContactTitle";
    '            workParam = workCommand.Parameters.Add(new SqlParameter("@Address", SqlDbType.NVarChar, 60));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "Address";
    '            workParam = workCommand.Parameters.Add(new SqlParameter("@City", SqlDbType.NVarChar, 15));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "City";
    '            workParam = workCommand.Parameters.Add(new SqlParameter("@Region", SqlDbType.NVarChar, 15));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "Region";
    '            workParam = workCommand.Parameters.Add(new SqlParameter("@PostalCode", SqlDbType.NVarChar, 10));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "PostalCode";
    '            workParam = workCommand.Parameters.Add(new SqlParameter("@Country", SqlDbType.NVarChar, 15));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "Country";
    '            workParam = workCommand.Parameters.Add(new SqlParameter("@Phone", SqlDbType.NVarChar, 24));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "Phone";
    '            workParam = workCommand.Parameters.Add(new SqlParameter("@Fax", SqlDbType.NVarChar, 24));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "Fax";

    '        }

    '        private void buildCustomersDeleteParams(SqlCommand workCommand)
    '        {

    '            SqlParameter workParam = null;
    '            workParam = workCommand.Parameters.Add(new SqlParameter("@CustomerID", SqlDbType.NChar, 5));
    '            workParam.Direction = ParameterDirection.Input;
    '            workParam.SourceColumn = "CustomerID";

    '        }

    '    }

    '}




    'At the top of the Code window, add the following Imports statements:
    'Imports System.Web.Services
    'Imports System.Data
    'Imports System.Data.SqlClient

    'After the following lines of code 
    '    Public Class Service1
    '        Inherits System.Web.Services

    'add the following code:
    '        <WebMethod()> Public Function GetData() As DataSet
    '            Dim con As New SqlConnection("server=<SQLServer>;uid=<myid>;pwd=<mypwd>;database=northwind")
    '            'Pull back the recent orders for the parent rows.
    '            Dim daOrders As New SqlDataAdapter("SELECT * FROM Orders WHERE OrderDate >= '05/01/1998'", con)
    '            'Get only the appropriate child rows for the preceding parent rows.
    '        Dim daDetails As New SqlDataAdapter("SELECT * FROM [Order Details] _
    '            WHERE OrderID in ( SELECT OrderID FROM Orders WHERE OrderDate >= '05/01/1998')", con)
    '            Dim ds As New DataSet
    '            Dim dt As DataTable
    '            Dim dc As DataColumn
    '            Dim cb As SqlCommandBuilder
    '            '
    '            ' Fill DataSet and set DataRelation for navigating in the DataGrid.
    '            '
    '            con.Open()
    '            daOrders.FillSchema(ds, SchemaType.Mapped, "Orders")
    '            daOrders.Fill(ds, "Orders")
    '            daDetails.FillSchema(ds, SchemaType.Mapped, "Details")
    '            daDetails.Fill(ds, "Details")
    '            ds.Relations.Add("OrdDetail", ds.Tables!Orders.Columns!OrderID, ds.Tables!Details.Columns!OrderID)
    '            '
    '            ' Set AutoNumber properties in the Orders DataTable.
    '            '
    '            dt = ds.Tables!Orders
    '            dc = dt.Columns!OrderID
    '            dc.AutoIncrement = True
    '            dc.AutoIncrementSeed = -1
    '            dc.AutoIncrementStep = -1
    '            '
    '            'Return the DataSet to the client.
    '            '
    '            GetData = ds
    '        End Function

    '        <WebMethod()> Public Function UpdateData(ByVal ds As DataSet) As DataSet
    '            Dim con As New SqlConnection("server=<SQLServer>;uid=<myid>;pwd=<mypwd>;database=northwind")
    '            Dim daOrders As New SqlDataAdapter("SELECT * FROM Orders WHERE OrderDate >= '05/01/1998'", con)
    '        Dim daDetails As New SqlDataAdapter("SELECT * FROM [Order Details] _
    '            WHERE OrderID in ( SELECT OrderID FROM Orders WHERE OrderDate >= '05/01/1998')", con)
    '            '
    '            ' Get commands for Orders table.
    '            ' Reselect record after insert to get new Identity value.
    '            ' Must get the schema, which you did in GetData(), before getting commands, 
    '            ' or the Command Builder will try to insert new rows, based 
    '            ' on the Identity column.
    '            '
    '            con.Open()
    '            Dim cb As SqlCommandBuilder
    '            cb = New SqlCommandBuilder(daOrders)
    '            daOrders.UpdateCommand = cb.GetUpdateCommand
    '            daOrders.DeleteCommand = cb.GetDeleteCommand
    '            daOrders.InsertCommand = cb.GetInsertCommand
    '            daOrders.InsertCommand.CommandText &= "; Select * From Orders Where OrderID = @@IDENTITY"
    '            '
    '            ' UpdateRowSource tells the DataAdapter that there will be a re-selected record.
    '            '
    '            daOrders.InsertCommand.UpdatedRowSource = UpdateRowSource.FirstReturnedRecord
    '            cb = Nothing
    '            '
    '            ' Get commands for Order Details table.
    '            '
    '            ' Must set the QuotePrefix and QuoteSuffix or the CommandBuilder won't put [ ]
    '            ' around the table name.
    '            '
    '            cb = New SqlCommandBuilder(daDetails)
    '            cb.QuotePrefix = "["
    '            cb.QuoteSuffix = "]"
    '            daDetails.UpdateCommand = cb.GetUpdateCommand
    '            daDetails.DeleteCommand = cb.GetDeleteCommand
    '            daDetails.InsertCommand = cb.GetInsertCommand
    '            cb = Nothing
    '            '
    '            ' Create a new DataAdapter based on the original one to prevent the
    '            ' CommandBuilder from modifying the SQL statements, 
    '            ' specifically the custom InsertCommand.
    '            '
    '            ' You don't need this if you roll your own commands and parameters or use 
    '            ' the visual tools to do it.
    '            '
    '            Dim daOrd2 As New SqlDataAdapter
    '            daOrd2.DeleteCommand = daOrders.DeleteCommand
    '            daOrd2.InsertCommand = daOrders.InsertCommand
    '            daOrd2.UpdateCommand = daOrders.UpdateCommand
    '            '
    '            ' Use a delegate to prevent AcceptChanges from occurring on Deletes & Inserts.

    '            ' This is for a limitation of the DataAdapter; see Q313540. 
    '            '
    '            AddHandler daOrd2.RowUpdated, AddressOf daOrd2_MyRowUpdated

    '            '
    '            AddHandler daDetails.RowUpdated, AddressOf dadetails_MyRowUpdated
    '            '
    '            'See Q313483 INFO: Roadmap for ADO.NET DataAdapter Objects. 

    '            daDetails.Update(GetDeletedRows(ds.Tables("details")))
    '            daOrd2.Update(GetDeletedRows(ds.Tables("orders")))

    '            daOrd2.Update(ds.Tables("orders").Select("", "", DataViewRowState.ModifiedCurrent))
    '            daDetails.Update(ds.Tables("details").Select("", "", DataViewRowState.ModifiedCurrent))

    '            daOrd2.Update(ds.Tables("orders").Select("", "", DataViewRowState.Added))
    '            '


    '            ' Otherwise, you see an integrity violation, because of the parent row's
    '            ' orphaned child rows. You get an orphaned child row temporarily, because
    '            ' you store the original pseudo foreign key back to the child row and issue
    '            ' an AcceptChanges to force it to the OriginalValue property. You then restore
    '            ' the actual foreign key value back to the row without AcceptChanges(). This
    '            ' puts the child row in the correct state to be merged with the original DataSet
    '            ' on the client. All of this functionality is handled in the RowUpdated delegate
    '            ' of the Order Details DataAdapter (see the daOrd2_MyRowUpdated procedure, which follows this procedure).
    '            ds.EnforceConstraints = False
    '            daDetails.Update(ds.Tables("details").Select("", "", DataViewRowState.Added))
    '            'Turn the integrity checking back on, because you turned it off earlier.
    '            ds.EnforceConstraints = True

    '            con.Close()
    '            'Send the DataSet back to the client to have the DataSet merged back in.
    '            UpdateData = ds

    '        End Function

    '        Private Sub daOrd2_MyRowUpdated(ByVal sender As Object, ByVal e As System.Data.SqlClient.SqlRowUpdatedEventArgs)
    '            If e.StatementType = StatementType.Insert Then e.Status = UpdateStatus.SkipCurrentRow
    '            If e.StatementType = StatementType.Delete Then e.Status = UpdateStatus.SkipCurrentRow
    '        End Sub

    '        Private Sub dadetails_MyRowUpdated(ByVal sender As Object, ByVal e As System.Data.SqlClient.SqlRowUpdatedEventArgs)

    '            ' When the primary key propagates down to the child row's foreign key field, the field
    '            ' does not receive an OriginalValue with pseudo key value and a CurrentValue with the 
    '            ' actual key value. Therefore, when the merge occurs, this row is  appended to the DataSet
    '            ' on the client tier, instead of being merged with the original row that was added.
    '            If e.StatementType = StatementType.Insert Then
    '                'Don't allow the AcceptChanges to occur on this row.
    '                e.Status = UpdateStatus.SkipCurrentRow
    '                ' Get the Current actual primary key value, so you can plug it back
    '                ' in after you get the correct original value that was generated for the child row.
    '                Dim currentkey As Integer = e.Row("OrderID") '.GetParentRow("OrdDetail")("OrderID", DataRowVersion.Current)
    '                ' This is where you get a correct original value key stored to the child row. You yank
    '                ' the original pseudo key value from the parent, plug it in as the child row's primary key
    '                ' field, and accept changes on it. Specifically, this is why you turned off EnforceConstraints.
    '                e.Row!OrderID = e.Row.GetParentRow("OrdDetail")("OrderID", DataRowVersion.Original)
    '                e.Row.AcceptChanges()
    '                ' Now store the actual primary key value back into the foreign key column of the child row.
    '                e.Row!OrderID = currentkey
    '            End If
    '            If e.StatementType = StatementType.Delete Then e.Status = UpdateStatus.SkipCurrentRow
    '        End Sub



    '        Private Function GetDeletedRows(ByVal dt As DataTable) As DataRow()
    '            Dim Rows() As DataRow
    '            If dt Is Nothing Then Return Rows
    '            Rows = dt.Select("", "", DataViewRowState.Deleted)
    '            If Rows.Length = 0 OrElse Not (Rows(0) Is Nothing) Then Return Rows
    '            '
    '            ' Workaround:
    '            ' With a remoted DataSet, Select returns the array elements
    '            ' filled with Nothing/null, instead of DataRow objects.
    '            '
    '            Dim r As DataRow, I As Integer = 0
    '            For Each r In dt.Rows
    '                If r.RowState = DataRowState.Deleted Then
    '                    Rows(I) = r
    '                    I += 1
    '                End If
    '            Next
    '            Return Rows
    '        End Function

    '    End Class




    'Использование готового решения
    'К этому моменту готов основной проект – PassportEditControl. В нем находится целых пять public-классов:

    'Passport. 
    'PassportConverter. 
    'PassportEdit. 
    'DataGridViewPassportCell. 
    'DataGridViewPassportColumn. 

    'Попробуем провести "полевые испытания" разработанного решения. Создаем новый проект типа Windows Application и называем его PassportEditControl_Test. 
    'В его References добавляем ссылку на библиотеку PassportEditControl.dll (результат компиляции предыдущего проекта). 
    'Кидаем на форму grid. Через smart tag последнего вызываем диалоговое окно 'Add Columns'. В нем, прежде всего, 
    'добавляем пару обычных текстовых колонок, в которых будут отображаться имя и фамилия, и колонку типа DataGridViewPassportColumn. 
    'Когда мы добавили ссылку на сборку PassportEditControl.dll, дизайнер увидел определенный в ней тип колонки (DataGridViewPassportColumn) и теперь мы можем работать с ней, как со встроенной колонкой.
    'Схема таблицы определена. Идем в обработчик события загрузки формы и в нем добавляем в grid 2 пустых строчки. 
    'В этих строчках колонка DataGridViewPassportColumn будет содержать null. Затем добавляем заполненную строчку и еще одну пустую. 
    'Далее для большего удобства работы и исключения горизонтального скроллинга устанавливаем ширину последней колонки по ширине нашего редактора, а ширину всей формы – по сумме ширин всех колонок grid-а.
    'Private Sub ParametrDataLoad()
    '    Dim pe As New ParametrEdit()
    '    'Me.dgvParametr.Rows.Add(2)
    '    'Me.dgvParametr.Rows.Add("Петр", "Петров", New Parametr("55", "001287", New DateTime(1991, 8, 19)))
    '    'Me.dgvParametr.Rows.Add("Обороты N", Nothing, True, New Parametr(999))
    '    'dgvParametr.Rows(dgvParametr.RowCount - 2).Cells("ValueConst").ReadOnly = False


    '    'dgvParametr.Rows(dgvParametr.Rows.Add(Nothing, Nothing, False, Nothing)).Cells("ValueConst").ReadOnly = True
    '    'dgvParametr.Rows(dgvParametr.Rows.Add(Nothing, Nothing, False, Nothing)).Cells("ValueConst").ReadOnly = True

    '    'dgvParametr.Rows(dgvParametr.Rows.Add("Обороты N", Nothing, True, New Parametr(999))).Cells("ValueConst").ReadOnly = False

    '    ''dgvParametr.Columns("ValueConst").ReadOnly = True

    '    'dgvParametr.Rows(dgvParametr.Rows.Add(Nothing, Nothing, False, Nothing)).Cells("ValueConst").ReadOnly = True
    '    'dgvParametr.Rows(dgvParametr.Rows.Add(Nothing, Nothing, False, Nothing)).Cells("ValueConst").ReadOnly = True

    '    'Me.dgvParametr.Rows.Add(4)
    '    Me.dgvParametr.Columns(2).Width = pe.Width + 2
    '    Dim totalWidth As Integer = 0
    '    For Each col As DataGridViewColumn In Me.dgvParametr.Columns
    '        totalWidth += (col.Width + col.DividerWidth)
    '    Next
    '    Me.ClientSize = New Size(totalWidth + Me.dgvParametr.RowHeadersWidth + 5, Me.ClientSize.Height)
    'End Sub

    'На форме также присутствует кнопка, отображающая содержимое последней колонки текущей строки.
    'Private Sub _btShowVal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btShowVal.Click
    '    Dim rowIdx As Integer = dgvParametr.CurrentCell.RowIndex
    '    Dim pas As Parametr = TryCast(dgvParametr.Rows(rowIdx).Cells("ValueConst").Value, Parametr)
    '    'Dim info As String = (IIf(pas Is Nothing, "Ячейка не содержит объект типа Parametr!", "Ячейка содержит объект типа Passport с реквизитами:" + Environment.NewLine + "Серия - " + pas.Series + Environment.NewLine + "№ - " + pas.Number + Environment.NewLine + "ДатаВыдачи - " + pas.IssueDate.ToShortDateString()))
    '    Dim info As String
    '    If pas Is Nothing Then
    '        info = "Ячейка не содержит объект типа Parametr!"
    '    Else
    '        'info = "Ячейка содержит объект типа Parametr с реквизитами:" + Environment.NewLine + "Серия - " + pas.Series + Environment.NewLine + "№ - " + pas.Number + Environment.NewLine + "ДатаВыдачи - " + pas.IssueDate.ToShortDateString()
    '        info = "Ячейка содержит объект типа Parametr с реквизитами:" + Environment.NewLine + "Значене - " + pas.ValueParametr.ToString
    '    End If
    '    MessageBox.Show(info, "Info about cell [2]\[" + rowIdx.ToString() + "]")
    'End Sub

    'Проверяем, что при входе в фазу редактирования все три редактирующих control-а инициализируются значениями из текущего объекта Passport. 
    'Удостоверяемся, что добавление новых записей также вопросов не вызывает. Наконец, для контроля, щелкая по кнопке, убеждаемся, что все вводимые нами данные действительно попадают в ячейку. 

    'Private Sub dgvParametr_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvParametr.CellContentClick
    '    'при заполнении сетки ReadOnly ячеек сделать в соответствии со значением чека
    '    'If ТаблицаЗаполнена AndAlso e.ColumnIndex = 1 Then
    '    If e.ColumnIndex = 2 Then

    '        'изменения управления по логике или по значению
    '        'If CType(DataGridViewValuePower.Rows(e.RowIndex).Cells(e.ColumnIndex), DataGridViewCheckBoxCell).Value = True Then
    '        'If CBool(DataGridViewValuePower.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) = True Then
    '        '    MessageBox.Show("True")
    '        'Else
    '        '    MessageBox.Show("false")
    '        'End If
    '        dgvParametr.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = Not CBool(dgvParametr.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)
    '        'следующую ячейку сделать доступной
    '        dgvParametr.Rows(e.RowIndex).Cells(e.ColumnIndex + 1).ReadOnly = Not CBool(dgvParametr.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)

    '        If CBool(dgvParametr.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) Then
    '            dgvParametr.Rows(e.RowIndex).Cells("ValueConst").Style.BackColor = System.Drawing.SystemColors.Window
    '        Else
    '            dgvParametr.Rows(e.RowIndex).Cells("ValueConst").Style.BackColor = System.Drawing.SystemColors.InactiveBorder
    '        End If



    '        'mColumnIndex = e.ColumnIndex
    '        'mRowIndex = e.RowIndex

    '        'Timer1.Enabled = True
    '        'Dim cell1 As DataGridViewCheckBoxCell = CType(DataGridViewValuePower.Rows(e.RowIndex).Cells(e.ColumnIndex).Clone, DataGridViewCheckBoxCell)
    '        'If CBool(cell1.Value) = True Then
    '        '    MessageBox.Show("True")
    '        'Else
    '        '    MessageBox.Show("false")
    '        'End If
    '        'DataGridViewValuePower.EndEdit()
    '    End If
    'End Sub
End Module
