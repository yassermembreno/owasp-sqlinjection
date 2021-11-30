using System;
using System.Data;
using owasp_sqlinjection.Domain.Entities;
using owasp_sqlinjection.Domain.Interfaces;

namespace owas_sqlinjection.Infrastructure.Repositories
{
    public class UnsecureCustomerRepository : ICustomerRepository
    {
        private IDBContext dBContext;

        public UnsecureCustomerRepository(IDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public void Create(Customer t)
        {
            string sql = $@"INSERT INTO Customer(Names, Lastnames, DNI, Phone, Address, Latitude, Longitude, CreatedOn, ModifiedOn)
                            VALUES('{t.Names}', '{t.Lastnames}', '{t.DNI}', '{t.Phone}', '{t.Address}', {t.Latitude}, {t.Longitude}, '{t.CreatedOn.ToString("yyyy-MM-dd H:mm:ss")}', '{t.ModifiedOn.ToString("yyyy-MM-dd H:mm:ss")}')";
            try
            {
                dBContext.ExecuteNonQuery(sql, null);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public bool Delete(Customer t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> FindAll()
        {
            List<Customer>? customers = null;
            string sql = $@"SELECT * FROM Customer";

            try
            {
                DataTable dataTable = dBContext.QueryList(sql, null);

                if (dataTable == null)
                {
                    return customers;
                }

                customers = ReaderToList(dataTable);

            }
            catch (Exception)
            {
                throw;
            }

            return customers;
        }

        public Customer FindByDNI(string dni)
        {
            Customer? customer = null;
            string sql = $@"SELECT * FROM Customer
                             WHERE DNI = '{dni}'";

            try
            {
                DataTable dt = dBContext.QueryList(sql, null);

                if (dt == null)
                {
                    return customer;
                }

                foreach (DataRow row in dt.Rows)
                {
                    customer = CreateCustomer(row);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

            return customer;
        }

        public IEnumerable<Customer> FindByNames(string names)
        {
            List<Customer> customers = new List<Customer>();
            string sql = $@"SELECT * FROM Customer
                             WHERE Names = '{names}'";

            try
            {
                DataTable dt = dBContext.QueryList(sql, null);

                if (dt == null)
                {
                    return customers;
                }

                foreach (DataRow row in dt.Rows)
                {
                    customers.Add(CreateCustomer(row));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }


            return customers;
        }

        public int Update(Customer t)
        {
            throw new NotImplementedException();
        }

        #region Private method
        private List<Customer> ReaderToList(DataTable dataTable)
        {
            List<Customer> customers = new List<Customer>();

            foreach (DataRow row in dataTable.Rows)
            {
                customers.Add(CreateCustomer(row));
            }

            return customers;
        }

        private Customer CreateCustomer(DataRow row)
        {
            Customer customer = new Customer()
            {
                CustomerId = int.Parse(string.IsNullOrEmpty(value: row["CustomerId"].ToString()) ? "0" : row["CustomerId"].ToString()),
                Names = row["Names"].ToString(),
                Lastnames = row["Lastnames"].ToString(),
                DNI = row["DNI"].ToString(),
                Phone = row["Phone"].ToString(),
                Address = row["Address"].ToString(),
                Latitude = Single.Parse(string.IsNullOrEmpty(row["Latitude"].ToString()) ? "0" : row["Latitude"].ToString()),
                Longitude = Single.Parse(string.IsNullOrEmpty(row["Longitude"].ToString()) ? "0" : row["Longitude"].ToString()),
                CreatedOn = (DateTime)(row["CreatedOn"] is DBNull ? DateTime.MinValue : row["CreatedOn"]),
                ModifiedOn = (DateTime)(row["ModifiedOn"] is DBNull ? DateTime.MinValue : row["ModifiedOn"]),
            };

            return customer;
        }
        #endregion

    }
}

