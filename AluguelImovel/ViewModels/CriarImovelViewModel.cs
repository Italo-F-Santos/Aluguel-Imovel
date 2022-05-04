using System.ComponentModel.DataAnnotations;

namespace AluguelImovel
{
    public class CriarImovelViewModel
    {
        [Required]
        public int Cep { get; set; }

        [Required]
        public string Descricao { get; set; }


    }
}
