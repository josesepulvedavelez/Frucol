using System;
using System.Threading.Tasks;
using Frucol.Server.Models;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;

namespace Frucol.Server.Data
{
    public class ProductorData
    {
        private readonly string _cadena;
        SqlConnection conexion;
        SqlCommand comando;
        SqlDataReader lector;
        int result;

        string queryInsert = "INSERT INTO Productor (Nombres, Apellidos, Cedula, AnioNacimiento, Edad, TipoEdad, Genero) " +
                                    "VALUES(@Nombres, @Apellidos, @Cedula, @AnioNacimiento, @Edad, @TipoEdad, @Genero)";

        string queryUpdate = "UPDATE Productor " +
                                "SET Nombres=@Nombres, Apellidos=@Apellidos, Cedula=@Cedula, AnioNacimiento=@AnioNacimiento, Edad=@Edad, TipoEdad=@TipoEdad, Genero=@Genero " +
                                    "WHERE ProductorId=@ProductorId";

        string queryDelete = "DELETE FROM Productor WHERE ProductorId=@ProductorId";

        string QuerySelect = "SELECT * FROM Productor";

        string QuerySelectId = "SELECT * FROM Productor WHERE ProductorId=@ProductorId";

        public ProductorData(IConfiguration configuration)
        {
            _cadena = configuration.GetConnectionString("cadena");
        }

        public async Task<int> Insertar(ProductorModel productorModel)
        {
            using(conexion = new SqlConnection(_cadena))
            {
                using(comando = new SqlCommand(queryInsert, conexion))
                {
                    comando.Parameters.AddWithValue("@Nombres", productorModel.Nombres);
                    comando.Parameters.AddWithValue("@Apellidos", productorModel.Apellidos);
                    comando.Parameters.AddWithValue("@Cedula", productorModel.Cedula);
                    comando.Parameters.AddWithValue("@AnioNacimiento", productorModel.AnioNacimiento);
                    comando.Parameters.AddWithValue("@Edad", productorModel.Edad);
                    comando.Parameters.AddWithValue("@TipoEdad", productorModel.TipoEdad);
                    comando.Parameters.AddWithValue("@Genero", productorModel.Genero);

                    conexion.Open();
                    result = await comando.ExecuteNonQueryAsync();
                    conexion.Close();
                }
            }

            return result;
        }

        public async Task<int> Actualizar(ProductorModel productorModel)
        {
            using (conexion = new SqlConnection(_cadena))
            {
                using (comando = new SqlCommand(queryUpdate, conexion))
                {
                    comando.Parameters.AddWithValue("@ProductorId", productorModel.ProductorId);
                    comando.Parameters.AddWithValue("@Nombres", productorModel.Nombres);
                    comando.Parameters.AddWithValue("@Apellidos", productorModel.Apellidos);
                    comando.Parameters.AddWithValue("@Cedula", productorModel.Cedula);
                    comando.Parameters.AddWithValue("@AnioNacimiento", productorModel.AnioNacimiento);
                    comando.Parameters.AddWithValue("@TipoEdad", productorModel.TipoEdad);
                    comando.Parameters.AddWithValue("@Edad", productorModel.Edad);
                    comando.Parameters.AddWithValue("@Genero", productorModel.Genero);

                    conexion.Open();
                    result = await comando.ExecuteNonQueryAsync();
                    conexion.Close();
                }
            }

            return result;
        }

        public async Task<int> Eliminar(int id)
        {
            using (conexion = new SqlConnection(_cadena))
            {
                using (comando = new SqlCommand(queryDelete, conexion))
                {
                    comando.Parameters.AddWithValue("@ProductorId", id);

                    conexion.Open();
                    result = await comando.ExecuteNonQueryAsync();
                    conexion.Close();
                }
            }

            return result;
        }

        public async Task<List<ProductorModel>> Seleccionar()
        {
            List<ProductorModel> _lstProductor = new List<ProductorModel>();

            using (conexion = new SqlConnection(_cadena))
            {
                conexion.Open();

                using (comando = new SqlCommand(QuerySelect, conexion))
                {
                    lector = comando.ExecuteReader();

                    while (await lector.ReadAsync())
                    {
                        ProductorModel productorModel = new ProductorModel()
                        {
                            Nombres = Convert.ToString(lector["Nombres"]),
                            Apellidos = Convert.ToString(lector["Apellidos"]),
                            Cedula = Convert.ToString(lector["Cedula"]),
                            TipoEdad = Convert.ToString(lector["TipoEdad"]),
                            Edad = Convert.ToInt16(lector["Edad"]),
                            AnioNacimiento = Convert.ToDateTime(lector["AnioNacimiento"]),
                            Genero = Convert.ToString(lector["Genero"]),
                            ProductorId = Convert.ToInt16(lector["ProductorId"])
                        };

                        _lstProductor.Add(productorModel);
                    }
                }
            }

            return _lstProductor;
        }

        public async Task<ProductorModel> SeleccionarId(int id)
        {
            ProductorModel productorModel = new ProductorModel();

            using (conexion = new SqlConnection(_cadena))
            {
                await conexion.OpenAsync();

                using (comando = new SqlCommand(QuerySelectId, conexion))
                {
                    comando.Parameters.AddWithValue("@ProductorId", id);
                    lector = comando.ExecuteReader();

                    while (await lector.ReadAsync())
                    {
                        productorModel.Nombres = Convert.ToString(lector["Nombres"]);
                        productorModel.Apellidos = Convert.ToString(lector["Apellidos"]);
                        productorModel.Cedula = Convert.ToString(lector["Cedula"]);
                        productorModel.TipoEdad = Convert.ToString(lector["TipoEdad"]);
                        productorModel.Edad = Convert.ToInt16(lector["Edad"]);
                        productorModel.AnioNacimiento = Convert.ToDateTime(lector["AnioNacimiento"]);
                        productorModel.Genero = Convert.ToString(lector["Genero"]);
                        productorModel.ProductorId = Convert.ToInt16(lector["ProductorId"]);
                    }
                }
            }

            return productorModel;
        }
    }
}
