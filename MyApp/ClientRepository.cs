﻿using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace MyApp
{
    public interface IClientRepository
    {
        Client GetById(int id);
    }

    public class ClientRepository : IClientRepository
    {
        public Client GetById(int id)
        {
            Client client = null;
            var connectionString = ConfigurationManager.ConnectionStrings["appDatabase"].ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "getClientById"
                };

                var parameter = new SqlParameter("@ClientId", SqlDbType.Int) { Value = id };
                command.Parameters.Add(parameter);

                connection.Open();
                var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    client = new Client
                                      {
                                          Id = int.Parse(reader["ClientId"].ToString()),
                                          Name = reader["Name"].ToString(),
                                          ClientStatus = (ClientStatus)int.Parse(reader["ClientStatusId"].ToString())
                                      };
                }
            }

            if (client == null) throw new System.Exception("Client not found in repository.");

            return client;
        }
    }
}
