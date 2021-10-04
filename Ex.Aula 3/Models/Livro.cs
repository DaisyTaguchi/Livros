using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExemploAula3.Models
{
    public class Livro
    {
        [DisplayName("Identificador de livro")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatorio")]
        [DisplayName("Ti­tulo do livro")]
        [MinLength(3, ErrorMessage = "O ti­tulo do livro deve possuir, ao menos, 3 caracteres")]
        [MaxLength(8)]

        public string Titulo { get; set; }

        [Required(ErrorMessage = "Data obrigatoria")]
        [DisplayName("Data de publicação")]
        [DataType(DataType.Date)]
        
        public DateTime DataPublicacao { get; set; }

        [Required(ErrorMessage = "Campo obrigatorio")]
        [DisplayName("Nome da editora")]
        public string Editora { get; set; }

        [Required(ErrorMessage = "Campo obrigatorio")]
        [DisplayName("Ti­po do item(Livro/Revista/Publicação Científica)")]
        public string Tipo { get; set; }

        [Required(ErrorMessage = "Campo obrigatorio")]
        [DisplayName("Quantidade de página")]
        [MinLength(1, ErrorMessage = "O livro deve possuir, ao menos, 1 página")]
        public int Pagina { get; set; }

        
    }
}

