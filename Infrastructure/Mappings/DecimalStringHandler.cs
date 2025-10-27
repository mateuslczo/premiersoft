using Dapper;
using System.Data;
using System.Globalization;

public class DecimalStringHandler :SqlMapper.TypeHandler<decimal>
{
	/// <summary>
	/// Converte uma string para um valor decimal (aplicação para banco)
	/// </summary>
	/// <param name="value"></param>
	/// <returns></returns>
	public override decimal Parse(object value)
	{

		return decimal.Parse(value.ToString(), CultureInfo.InvariantCulture);
	}

	/// <summary>
	/// Converte um tipo decimal para string (banco para aplicação)
	/// </summary>
	/// <param name="parameter"></param>
	/// <param name="value"></param>
	public override void SetValue(IDbDataParameter parameter, decimal value)
	{
		parameter.Value = value.ToString(CultureInfo.InvariantCulture);
	}
}
