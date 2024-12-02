namespace ProjetoMicroondas.Dominio.Entidades
{
	public class Microondas
	{
		public int TempoSegundos { get; set; }
		public int Potencia { get; set; }
		public string Estado { get; set; }
		public DateTime? InicioAquecimento { get; set; }

		public Microondas()
		{
			TempoSegundos = 0;
			Potencia = 10;
			Estado = "Parado";
		}
	}
}
