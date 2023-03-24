using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Banco
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.BancoContext context = new DL.BancoContext())
                {
                    var query = context.Bancos.FromSqlRaw("BancoGetAll").ToList();

                    if (query != null)
                    {
                        result.Objects = new List<object>();

                        foreach (var obj in query)
                        {
                            ML.Banco banco = new ML.Banco();
                            banco.IdBanco = obj.IdBanco;
                            banco.Nombre = obj.Nombre;
                            banco.NoEmpleados = obj.NoEmpleados.Value;
                            banco.NoSucursales = obj.NoSucursales.Value;
                            banco.Pais = new ML.Pais();
                            banco.Pais.IdPais = obj.IdPais.Value;
                            banco.Capital = obj.Capital.Value;
                            banco.RazonSocial = new ML.RazonSocial();
                            banco.RazonSocial.IdRazonSocial = obj.IdRazonSocial.Value;
                            banco.NoClientes = obj.NoClientes.Value;


                            result.Objects.Add(banco);

                        }
                    }
                }
                result.Correct = true;

            }
            catch (Exception ex)
            {
                result.ex = ex;
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result Add(ML.Banco banco)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.BancoContext context = new DL.BancoContext())
                {
                    var query = context.Database.ExecuteSqlRaw("BancoAdd");

                    if (query != null)
                    {
                        result.Correct = true;
                    }
                    else 
                    {
                        result.Correct = false;
                    }
                }            
            }
            catch (Exception ex)
            {
                result.ex = ex;
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}
