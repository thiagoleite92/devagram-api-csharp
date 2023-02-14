using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using DevagramCSharp.Dtos;

namespace DevagramCSharp.Services
{
    public class CosmicService
    {
        public string EnviarImagem(ImagemDto imagemDto)
        {
            Stream imagem;
			imagem  = imagemDto.Imagem.OpenReadStream();

            var client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "JyK3jgU5i5ScNbneFjzBk8Vg8bTdnLtGuhjTOdOKt1CFPTCyOl");

			var request = new HttpRequestMessage(HttpMethod.Post, "file");

			var conteudo =  new MultipartFormDataContent
			{
				{ new StreamContent(imagem), "media", imagemDto.Nome}
			};

			request.Content = conteudo;

			var response = client.PostAsync("https://upload.cosmicjs.com/v2/buckets/devagram-sharp-devaria-sharp/media", request.Content).Result;

			var urlretorno = response.Content.ReadFromJsonAsync<CosmicRespostaDto>();

            return urlretorno.Result.media.url;
        }
    }
}