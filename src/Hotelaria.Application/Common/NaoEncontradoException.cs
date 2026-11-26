using System;
using System.Collections.Generic;
using System.Text;

namespace Hotelaria.Application.Common
{
    public class NaoEncontradoException(string entidade, Guid id) : Exception($"{entidade} com id '{id}' não foi encontrado.");
}
