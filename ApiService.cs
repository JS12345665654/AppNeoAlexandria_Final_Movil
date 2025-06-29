using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using Prueba.Utils;
using Prueba.ViewModels;
using Prueba.Models;
using System.Diagnostics;
using System.Text.Json;
using Microsoft.Maui.Devices;
using System.Text.Json.Serialization;

namespace Prueba
{
    public class ApiService
    {
        private static readonly string BASE_URL = "https://appneo.somee.com/api/";
        static HttpClient httpClient = new HttpClient() { Timeout = TimeSpan.FromSeconds(60) };

        // API Service - Libro
        public static async Task<List<Libros>> ObtenerTodosLibros()
        {
            string FINAL_URL = BASE_URL + "Libros";
            try
            {
                var response = await httpClient.GetAsync(FINAL_URL);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrWhiteSpace(jsonData))
                    {
                        // Inside the ApiService class
                        var responseObject = JsonSerializer.Deserialize<List<Libros>>(jsonData,
                            new JsonSerializerOptions
                            {
                                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                                WriteIndented = true
                            });
                        return responseObject!;
                    }
                    else
                    {
                        Exception exception = new Exception("Resource Not Found");
                        throw new Exception(exception.Message);
                    }
                }
                else
                {
                    Exception exception = new Exception("Request failed with status code " + response.StatusCode);
                    throw new Exception(exception.Message);
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        public static async Task<bool> CrearLibro(Libros crearLibro)
        {
            string FINAL_URL = BASE_URL + "Libros";
            try
            {
                var content = new StringContent(
                        JsonSerializer.Serialize(crearLibro),
                        Encoding.UTF8, "application/json"
                );

                var result = await httpClient.PostAsync(FINAL_URL, content).ConfigureAwait(false);

                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static async Task<List<Libros>> ObtenerLibroPorId(int IdLibro)
        {
            string URL = "https://appneo.somee.com/api/Libros/ObtenerLibroPorId/" + IdLibro;
            try
            {
                var response = await httpClient.GetAsync(URL);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrWhiteSpace(jsonData))
                    {
                        // Inside the ApiService class
                        var responseObject = JsonSerializer.Deserialize<List<Libros>>(jsonData,
                            new JsonSerializerOptions
                            {
                                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                                WriteIndented = true
                            });
                        return responseObject!;
                    }
                    else
                    {
                        Exception exception = new Exception("Resource Not Found");
                        throw new Exception(exception.Message);
                    }
                }
                else
                {
                    Exception exception = new Exception("Request failed with status code " + response.StatusCode);
                    throw new Exception(exception.Message);
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public static async Task<bool> ModificarLibro(Libros libros)
        {
            string FINAL_URL = BASE_URL + "Libros/" + libros.IdLibro;

            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", TokenStore.JwtToken);

            try
            {
                Debug.WriteLine($"[TOKEN ENVIADO] {TokenStore.JwtToken}");
                Debug.WriteLine($"[MODIFICANDO LIBRO] ID: {libros.IdLibro}");

                var content = new StringContent(
                    JsonSerializer.Serialize(libros),
                    Encoding.UTF8, "application/json"
                );

                var result = await httpClient.PutAsync(FINAL_URL, content).ConfigureAwait(false);

                Debug.WriteLine($"[RESULTADO PUT] Status: {(int)result.StatusCode} ({result.StatusCode})");

                return result.StatusCode == HttpStatusCode.OK || result.StatusCode == HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ERROR MODIFICAR LIBRO] {ex.Message}");
                throw new Exception($"Error al modificar libro: {ex.Message}");
            }
        }
        public static async Task<bool> EliminarLibro(int idLibro)
        {
            string FINAL_URL = BASE_URL + "Libros/" + idLibro;

            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", TokenStore.JwtToken);

            try
            {
                Debug.WriteLine($"[TOKEN ENVIADO] {TokenStore.JwtToken}");
                Debug.WriteLine($"[ELIMINANDO LIBRO] ID: {idLibro}");

                var result = await httpClient.DeleteAsync(FINAL_URL).ConfigureAwait(false);

                Debug.WriteLine($"[RESULTADO DELETE] Status: {(int)result.StatusCode} ({result.StatusCode})");

                return result.StatusCode == HttpStatusCode.OK || result.StatusCode == HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ERROR ELIMINAR LIBRO] {ex.Message}");
                throw new Exception($"Error al eliminar libro: {ex.Message}");
            }
        }

        //API Service - Usuarios
        public static async Task<List<Usuario>> ObtenerTodosUsuarios()
        {
            string FINAL_URL = BASE_URL + "Usuarios";
            try
            {
                var response = await httpClient.GetAsync(FINAL_URL);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrWhiteSpace(jsonData))
                    {
                        // Inside the ApiService class
                        var responseObject = JsonSerializer.Deserialize<List<Usuario>>(jsonData,
                            new JsonSerializerOptions
                            {
                                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                                WriteIndented = true
                            });
                        return responseObject!;
                    }
                    else
                    {
                        Exception exception = new Exception("Resource Not Found");
                        throw new Exception(exception.Message);
                    }
                }
                else
                {
                    Exception exception = new Exception("Request failed with status code " + response.StatusCode);
                    throw new Exception(exception.Message);
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        public static async Task<bool> CrearUsuarios(Usuario usuarios)
        {
            string FINAL_URL = BASE_URL + "Usuarios";
            try
            {
                var content = new StringContent(
                        JsonSerializer.Serialize(usuarios),
                        Encoding.UTF8, "application/json"
                );

                var result = await httpClient.PostAsync(FINAL_URL, content).ConfigureAwait(false);

                return result.StatusCode == HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static async Task<List<Usuario>> ObtenerUsuariosPorId(int IdUsuario)
        {
            string URL = "https://appneo.somee.com/api/Usuarios/ObtenerPorId/" + IdUsuario;
            try
            {
                var response = await httpClient.GetAsync(URL);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrWhiteSpace(jsonData))
                    {
                        // Inside the ApiService class
                        var responseObject = JsonSerializer.Deserialize<List<Usuario>>(jsonData,
                            new JsonSerializerOptions
                            {
                                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                                WriteIndented = true
                            });
                        return responseObject!;
                    }
                    else
                    {
                        Exception exception = new Exception("Resource Not Found");
                        throw new Exception(exception.Message);
                    }
                }
                else
                {
                    Exception exception = new Exception("Request failed with status code " + response.StatusCode);
                    throw new Exception(exception.Message);
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public static async Task<bool> ModificarUsuario(Usuario usuarios)
        {
            string FINAL_URL = BASE_URL + "Usuarios/" + usuarios.IdUsuario;

            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", TokenStore.JwtToken);
            try
            {
                var json = JsonSerializer.Serialize(usuarios, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var result = await httpClient.PutAsync(FINAL_URL, content).ConfigureAwait(false);

                return result.StatusCode == HttpStatusCode.OK || result.StatusCode == HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static async Task<bool> BorrarUsuario(int IdUsuario)
        {
            string FINAL_URL = BASE_URL + "Usuarios/" + IdUsuario;

            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", TokenStore.JwtToken);

            try
            {
                var result = await httpClient.DeleteAsync(FINAL_URL).ConfigureAwait(false);
                var body = await result.Content.ReadAsStringAsync();

                Console.WriteLine($"[DEBUG DELETE] Código: {result.StatusCode}, Body: {body}");

                return result.StatusCode == HttpStatusCode.OK || result.StatusCode == HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR DELETE] {ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        //API Service - Carrito
        public static async Task<List<Carrito>> ObtenerTodosCarritos()
        {
            string FINAL_URL = BASE_URL + "Carrito";
            try
            {
                var response = await httpClient.GetAsync(FINAL_URL);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrWhiteSpace(jsonData))
                    {
                        // Inside the ApiService class
                        var responseObject = JsonSerializer.Deserialize<List<Carrito>>(jsonData,
                            new JsonSerializerOptions
                            {
                                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                                WriteIndented = true
                            });
                        return responseObject!;
                    }
                    else
                    {
                        Exception exception = new Exception("Resource Not Found");
                        throw new Exception(exception.Message);
                    }
                }
                else
                {
                    Exception exception = new Exception("Request failed with status code " + response.StatusCode);
                    throw new Exception(exception.Message);
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        public static async Task<bool> CrearCarritos(Carrito carrito)
        {
            string FINAL_URL = BASE_URL + "Carrito";

            try
            {
                var json = JsonSerializer.Serialize(carrito, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true
                });

                Console.WriteLine("JSON a enviar:");
                Console.WriteLine(json);

                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(FINAL_URL, content).ConfigureAwait(false);

                string responseContent = await response.Content.ReadAsStringAsync();

                Console.WriteLine($"StatusCode: {response.StatusCode}");
                Console.WriteLine($"Contenido de respuesta: {responseContent}");

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    throw new Exception($"Status: {response.StatusCode}, Error: {responseContent}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Excepción al crear carrito: " + ex.Message);
                throw new Exception("Error al crear carrito: " + ex.Message);
            }
        }

        public static async Task<List<Carrito>> ObtenerCarritosPorId(int IdCarrito)
        {
            string URL = "https://appneo.somee.com/api/Carrito/ObtenerPorId/" + IdCarrito;
            try
            {
                var response = await httpClient.GetAsync(URL);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrWhiteSpace(jsonData))
                    {
                        // Inside the ApiService class
                        var responseObject = JsonSerializer.Deserialize<List<Carrito>>(jsonData,
                            new JsonSerializerOptions
                            {
                                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                                WriteIndented = true
                            });
                        return responseObject!;
                    }
                    else
                    {
                        Exception exception = new Exception("Resource Not Found");
                        throw new Exception(exception.Message);
                    }
                }
                else
                {
                    Exception exception = new Exception("Request failed with status code " + response.StatusCode);
                    throw new Exception(exception.Message);
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        public static async Task<bool> ModificarCarrito(Carrito carrito)
        {
            string FINAL_URL = BASE_URL + "Carrito/" + carrito.IdCarrito;
            try
            {
                var content = new StringContent(
                        JsonSerializer.Serialize(carrito),
                        Encoding.UTF8, "application/json"
                );

                var result = await httpClient.PutAsync(FINAL_URL, content).ConfigureAwait(false);

                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static async Task<bool> BorrarCarrito(int IdCarrito)
        {
            string FINAL_URL = BASE_URL + "Carrito/" + IdCarrito;

            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", TokenStore.JwtToken);

            try
            {
                var result = await httpClient.DeleteAsync(FINAL_URL).ConfigureAwait(false);

                return result.StatusCode == HttpStatusCode.OK || result.StatusCode == HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task<bool> CrearCarritoConDetalle(CarritoConDetalleDTO dto)
        {
            string FINAL_URL = BASE_URL + "Carrito/CrearConDetalle";
            try
            {
                var content = new StringContent(
                    JsonSerializer.Serialize(dto),
                    Encoding.UTF8, "application/json"
                );

                var result = await httpClient.PostAsync(FINAL_URL, content).ConfigureAwait(false);

                return result.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al crear carrito con detalle: {ex.Message}");
            }
        }

        // API Service - Detalle Carrito
        public static async Task<List<DetalleCarrito>> ObtenerDetalleCarritos()
        {
            string FINAL_URL = BASE_URL + "CarritoDetalle";
            try
            {
                var response = await httpClient.GetAsync(FINAL_URL);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrWhiteSpace(jsonData))
                    {
                        // Inside the ApiService class
                        var responseObject = JsonSerializer.Deserialize<List<DetalleCarrito>>(jsonData,
                            new JsonSerializerOptions
                            {
                                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                                WriteIndented = true
                            });
                        return responseObject!;
                    }
                    else
                    {
                        Exception exception = new Exception("Resource Not Found");
                        throw new Exception(exception.Message);
                    }
                }
                else
                {
                    Exception exception = new Exception("Request failed with status code " + response.StatusCode);
                    throw new Exception(exception.Message);
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        public static async Task<bool> CrearDetalleCarrito(DetalleCarrito detallecarrito)
        {
            string FINAL_URL = BASE_URL + "CarritoDetalle";
            try
            {
                var content = new StringContent(
                        JsonSerializer.Serialize(detallecarrito),
                        Encoding.UTF8, "application/json"
                );

                var result = await httpClient.PostAsync(FINAL_URL, content).ConfigureAwait(false);

                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static async Task<List<DetalleCarrito>> ObtenerDetalleCarritoPorId(int IdDetalleCarrito)
        {
            string URL = "https://appneo.somee.com/api/CarritoDetalle/ObtenerPorId/" + IdDetalleCarrito;

            try
            {
                var response = await httpClient.GetAsync(URL);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrWhiteSpace(jsonData))
                    {
                        // Inside the ApiService class
                        var responseObject = JsonSerializer.Deserialize<List<DetalleCarrito>>(jsonData,
                            new JsonSerializerOptions
                            {
                                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                                WriteIndented = true
                            });
                        return responseObject!;
                    }
                    else
                    {
                        Exception exception = new Exception("Resource Not Found");
                        throw new Exception(exception.Message);
                    }
                }
                else
                {
                    Exception exception = new Exception("Request failed with status code " + response.StatusCode);
                    throw new Exception(exception.Message);
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        public static async Task<bool> ModificarDetalleCarrito(DetalleCarrito detallecarrito)
        {
            string FINAL_URL = BASE_URL + "CarritoDetalle/" + detallecarrito.IdDetalleCarrito;
            try
            {
                var content = new StringContent(
                        JsonSerializer.Serialize(detallecarrito),
                        Encoding.UTF8, "application/json"
                );

                var result = await httpClient.PutAsync(FINAL_URL, content).ConfigureAwait(false);

                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task<bool> BorrarDetalleCarrito(int IdDetalleCarrito)
        {
            string FINAL_URL = BASE_URL + "CarritoDetalle/" + IdDetalleCarrito;

            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", TokenStore.JwtToken);

            try
            {
                var result = await httpClient.DeleteAsync(FINAL_URL).ConfigureAwait(false);

                return result.StatusCode == HttpStatusCode.OK || result.StatusCode == HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //API Service - Categoria
        public static async Task<List<Categoria>> ObtenerTodasCategorias()
        {
            string FINAL_URL = BASE_URL + "Categorias";
            try
            {
                var response = await httpClient.GetAsync(FINAL_URL);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrWhiteSpace(jsonData))
                    {
                        // Inside the ApiService class
                        var responseObject = JsonSerializer.Deserialize<List<Categoria>>(jsonData,
                            new JsonSerializerOptions
                            {
                                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                                WriteIndented = true
                            });
                        return responseObject!;
                    }
                    else
                    {
                        Exception exception = new Exception("Resource Not Found");
                        throw new Exception(exception.Message);
                    }
                }
                else
                {
                    Exception exception = new Exception("Request failed with status code " + response.StatusCode);
                    throw new Exception(exception.Message);
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        public static async Task<bool> AgregarCategoria(Categoria crearCategoria)
        {
            string FINAL_URL = BASE_URL + "Categorias";
            try
            {
                var content = new StringContent(
                        JsonSerializer.Serialize(crearCategoria),
                        Encoding.UTF8, "application/json"
                );

                var result = await httpClient.PostAsync(FINAL_URL, content).ConfigureAwait(false);

                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static async Task<List<Categoria>> ObtenerCategoriasPorId(int IdCategoria)
        {
            string URL = "https://appneo.somee.com/api/Categorias/ObtenerCategoriasPorId/" + IdCategoria;
            try
            {
                var response = await httpClient.GetAsync(URL);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrWhiteSpace(jsonData))
                    {
                        // Inside the ApiService class
                        var responseObject = JsonSerializer.Deserialize<List<Categoria>>(jsonData,
                            new JsonSerializerOptions
                            {
                                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                                WriteIndented = true
                            });
                        return responseObject!;
                    }
                    else
                    {
                        Exception exception = new Exception("Resource Not Found");
                        throw new Exception(exception.Message);
                    }
                }
                else
                {
                    Exception exception = new Exception("Request failed with status code " + response.StatusCode);
                    throw new Exception(exception.Message);
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        public static async Task<bool> ModificarCategoria(Categoria categoria)
        {
            string FINAL_URL = BASE_URL + "Categorias/" + categoria.IdCategoria;
            try
            {
                var content = new StringContent(
                        JsonSerializer.Serialize(categoria),
                        Encoding.UTF8, "application/json"
                );

                var result = await httpClient.PutAsync(FINAL_URL, content).ConfigureAwait(false);

                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task<bool> EliminarCategoria(int IdCategoria)
        {
            string FINAL_URL = BASE_URL + "Categorias/" + IdCategoria;

            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", TokenStore.JwtToken);

            try
            {
                var result = await httpClient.DeleteAsync(FINAL_URL).ConfigureAwait(false);

                return result.StatusCode == HttpStatusCode.OK || result.StatusCode == HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //API Service - Autores
        public static async Task<List<Autores>> ObtenerTodosAutores()
        {
            string FINAL_URL = BASE_URL + "Autores";
            try
            {
                var response = await httpClient.GetAsync(FINAL_URL);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrWhiteSpace(jsonData))
                    {
                        // Inside the ApiService class
                        var responseObject = JsonSerializer.Deserialize<List<Autores>>(jsonData,
                            new JsonSerializerOptions
                            {
                                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                                WriteIndented = true
                            });
                        return responseObject!;
                    }
                    else
                    {
                        Exception exception = new Exception("Resource Not Found");
                        throw new Exception(exception.Message);
                    }
                }
                else
                {
                    Exception exception = new Exception("Request failed with status code " + response.StatusCode);
                    throw new Exception(exception.Message);
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        public static async Task<bool> AgregarAutor(Autores autores)
        {
            string FINAL_URL = BASE_URL + "Autores";
            try
            {
                var content = new StringContent(
                        JsonSerializer.Serialize(autores),
                        Encoding.UTF8, "application/json"
                );

                var result = await httpClient.PostAsync(FINAL_URL, content).ConfigureAwait(false);

                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static async Task<List<Autores>> ObtenerAutoresPorId(int IdAutor)
        {
            string URL = "https://appneo.somee.com/api/Autores/ObtenerAutoresPorId/" + IdAutor;
            try
            {
                var response = await httpClient.GetAsync(URL);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrWhiteSpace(jsonData))
                    {
                        // Inside the ApiService class
                        var responseObject = JsonSerializer.Deserialize<List<Autores>>(jsonData,
                            new JsonSerializerOptions
                            {
                                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                                WriteIndented = true
                            });
                        return responseObject!;
                    }
                    else
                    {
                        Exception exception = new Exception("Resource Not Found");
                        throw new Exception(exception.Message);
                    }
                }
                else
                {
                    Exception exception = new Exception("Request failed with status code " + response.StatusCode);
                    throw new Exception(exception.Message);
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        public static async Task<bool> ModificarAutor(Autores autores)
        {
            string FINAL_URL = BASE_URL + "Autores/" + autores.IdAutor;
            try
            {
                var content = new StringContent(
                        JsonSerializer.Serialize(autores),
                        Encoding.UTF8, "application/json"
                    );

                var result = await httpClient.PutAsync(FINAL_URL, content).ConfigureAwait(false);

                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static async Task<bool> EliminarAutores(int IdAutor)
        {
            string FINAL_URL = BASE_URL + "Autores/" + IdAutor;

            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", TokenStore.JwtToken);

            try
            {
                var result = await httpClient.DeleteAsync(FINAL_URL).ConfigureAwait(false);

                return result.StatusCode == HttpStatusCode.OK || result.StatusCode == HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //API Service - Notas
        public static async Task<List<Notas>> ObtenerTodasNotas()
        {
            string FINAL_URL = BASE_URL + "Notas";
            try
            {
                var response = await httpClient.GetAsync(FINAL_URL);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrWhiteSpace(jsonData))
                    {
                        // Inside the ApiService class
                        var responseObject = JsonSerializer.Deserialize<List<Notas>>(jsonData,
                            new JsonSerializerOptions
                            {
                                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                                WriteIndented = true
                            });
                        return responseObject!;
                    }
                    else
                    {
                        Exception exception = new Exception("Resource Not Found");
                        throw new Exception(exception.Message);
                    }
                }
                else
                {
                    Exception exception = new Exception("Request failed with status code " + response.StatusCode);
                    throw new Exception(exception.Message);
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        public static async Task<bool> AgregarNota(Notas notas)
        {
            string FINAL_URL = BASE_URL + "Notas";
            try
            {
                var content = new StringContent(
                        JsonSerializer.Serialize(notas),
                        Encoding.UTF8, "application/json"
                    );

                var result = await httpClient.PostAsync(FINAL_URL, content).ConfigureAwait(false);

                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static async Task<List<Notas>> ObtenerNotasPorId(int IdNota)
        {
            string URL = "https://appneo.somee.com/api/Notas/ObtenerNotasPorId/" + IdNota;

            try
            {
                var response = await httpClient.GetAsync(URL);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrWhiteSpace(jsonData))
                    {
                        // Inside the ApiService class
                        var responseObject = JsonSerializer.Deserialize<List<Notas>>(jsonData,
                            new JsonSerializerOptions
                            {
                                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                                WriteIndented = true
                            });
                        return responseObject!;
                    }
                    else
                    {
                        Exception exception = new Exception("Resource Not Found");
                        throw new Exception(exception.Message);
                    }
                }
                else
                {
                    Exception exception = new Exception("Request failed with status code " + response.StatusCode);
                    throw new Exception(exception.Message);
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        public static async Task<bool> ModificarNota(Notas notas)
        {
            string FINAL_URL = BASE_URL + "Notas/" + notas.IdNota;
            try
            {
                var content = new StringContent(
                        JsonSerializer.Serialize(notas),
                        Encoding.UTF8, "application/json"
                );

                var result = await httpClient.PutAsync(FINAL_URL, content).ConfigureAwait(false);

                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static async Task<bool> EliminarNota(int IdNota)
        {
            string FINAL_URL = BASE_URL + "Notas/" + IdNota;

            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", TokenStore.JwtToken);

            try
            {
                var result = await httpClient.DeleteAsync(FINAL_URL).ConfigureAwait(false);

                return result.StatusCode == HttpStatusCode.OK || result.StatusCode == HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //API Service - Valoracion de los usuarios
        public static async Task<List<ValoraciondeUsuario>> ObtenerTodasValoraciones()
        {
            string FINAL_URL = BASE_URL + "ValoracionUsuarios";

            try
            {
                var response = await httpClient.GetAsync(FINAL_URL);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrWhiteSpace(jsonData))
                    {
                        // Inside the ApiService class
                        var responseObject = JsonSerializer.Deserialize<List<ValoraciondeUsuario>>(jsonData,
                            new JsonSerializerOptions
                            {
                                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                                WriteIndented = true
                            });
                        return responseObject!;
                    }
                    else
                    {
                        Exception exception = new Exception("Resource Not Found");
                        throw new Exception(exception.Message);
                    }
                }
                else
                {
                    Exception exception = new Exception("Request failed with status code " + response.StatusCode);
                    throw new Exception(exception.Message);
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        public static async Task<bool> AgregarValoracion(ValoraciondeUsuario valoraciondeUsuarios)
        {
            string FINAL_URL = BASE_URL + "ValoracionUsuarios";
            try
            {
                var content = new StringContent(
                        JsonSerializer.Serialize(valoraciondeUsuarios),
                        Encoding.UTF8, "application/json"
                );

                var result = await httpClient.PostAsync(FINAL_URL, content).ConfigureAwait(false);

                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task<List<ValoraciondeUsuario>> ObtenerValoracionesPorId(int IdValoracion)
        {
            string URL = "https://appneo.somee.com/api/ValoracionUsuarios/ObtenerValoriacionesPorId/" + IdValoracion;

            try
            {
                var response = await httpClient.GetAsync(URL);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrWhiteSpace(jsonData))
                    {
                        var responseObject = JsonSerializer.Deserialize<List<ValoraciondeUsuario>>(jsonData,
                            new JsonSerializerOptions
                            {
                                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                                WriteIndented = true
                            });
                        return responseObject!;
                    }
                    else
                    {
                        Exception exception = new Exception("Resource Not Found");
                        throw new Exception(exception.Message);
                    }
                }
                else
                {
                    Exception exception = new Exception("Request failed with status code " + response.StatusCode);
                    throw new Exception(exception.Message);
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public static async Task<bool> ModificarValoracion(ValoraciondeUsuario valoracionmodificada)
        {
            string FINAL_URL = BASE_URL + "ValoracionUsuarios/" + valoracionmodificada.IdValoracion;
            try
            {
                var content = new StringContent(
                        JsonSerializer.Serialize(valoracionmodificada),
                        Encoding.UTF8, "application/json"
                );

                var result = await httpClient.PutAsync(FINAL_URL, content).ConfigureAwait(false);

                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task<bool> EliminarValoracion(int IdValoracion)
        {
            string FINAL_URL = BASE_URL + "ValoracionUsuarios/" + IdValoracion;

            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", TokenStore.JwtToken);

            try
            {
                var result = await httpClient.DeleteAsync(FINAL_URL).ConfigureAwait(false);

                return result.StatusCode == HttpStatusCode.OK || result.StatusCode == HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task<List<ValoraciondeUsuario>> ObtenerValoracionesPorLibro(int idLibro)
        {
            string FINAL_URL = BASE_URL + $"ValoracionUsuarios/PorLibro/{idLibro}";
            try
            {
                var response = await httpClient.GetAsync(FINAL_URL).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return JsonSerializer.Deserialize<List<ValoraciondeUsuario>>(content,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
                }
                else
                {
                    return new List<ValoraciondeUsuario>();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener valoraciones: {ex.Message}");
            }
        }

        // API Service - Login
        public async Task<JwtResponseDTO> Login(string email, string contrasenia)
        {
            string FINAL_URL = BASE_URL + "Acceso/Login";

            try
            {
                var content = new StringContent(
                    JsonSerializer.Serialize(new { email, contrasenia }),
                    Encoding.UTF8, "application/json"
                );

                var result = await httpClient.PostAsync(FINAL_URL, content).ConfigureAwait(false);
                var jsonData = await result.Content.ReadAsStringAsync();

                if (!string.IsNullOrWhiteSpace(jsonData))
                {
                    var responseObject = JsonSerializer.Deserialize<JwtResponseDTO>(jsonData,
                        new JsonSerializerOptions
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                            WriteIndented = true
                        });

                    if (responseObject != null && responseObject.IsSuccess && !string.IsNullOrEmpty(responseObject.Token))
                    {
                        Prueba.Models.TokenStore.JwtToken = responseObject.Token;
                        return responseObject;
                    }
                    else
                    {
                        throw new Exception("Login inválido o token no recibido.");
                    }
                }
                else
                {
                    throw new Exception("Respuesta vacía del servidor.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al iniciar sesión: {ex.Message}");
            }
        }

        //Login - Registrar Usuario
        public async Task<LoginResponseDTO> Registrarse(UsuarioLoginDTO usuario)
        {
            string FINAL_URL = BASE_URL + "Acceso/Registrarse";

            try
            {
                var content = new StringContent(
                    JsonSerializer.Serialize(usuario),
                    Encoding.UTF8, "application/json"
                );

                var result = await httpClient.PostAsync(FINAL_URL, content).ConfigureAwait(false);
                var jsonData = await result.Content.ReadAsStringAsync();

                if (!string.IsNullOrWhiteSpace(jsonData))
                {
                    var responseObject = JsonSerializer.Deserialize<LoginResponseDTO>(jsonData,
                        new JsonSerializerOptions
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                            WriteIndented = true
                        });

                    return responseObject!;
                }
                else
                {
                    throw new Exception("Respuesta vacía del servidor.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al registrar usuario: {ex.Message}");
            }
        }
    }
}