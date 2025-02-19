﻿using AutoMapper;
using Azure.Core;
using Backend.Api.DTO.Game;
using Backend.Api.Helpers.Extensions;
using Backend.Api.Models;
using Backend.Api.Repositories.Interface;
using Backend.Api.Services.Interface;

namespace Backend.Api.Services.Implementations
{
    public class GameService : IGameService
    {
        readonly IGameRepository _rep;
        IWebHostEnvironment _env;
        IMapper _mapper;
        public GameService(IGameRepository rep, IMapper mapper, IWebHostEnvironment env)
        {
            _rep = rep;
            _mapper = mapper;
            _env = env;
        }
        public async Task<GetGameDto> CreateAsync(CreateGameDto dto, string baseUrl)
        {
            string imageUrl = null;
            List<string> imageUrls = new List<string>();


            // ✅ Handle Single Image Upload
            if (dto.Image != null)
            {
                string fileUrl = await SaveImageAsync(dto.Image);
                imageUrl = $"{baseUrl}{fileUrl}"; // ✅ Return full URL
            }

            // ✅ Handle Multiple Image Uploads
            if (dto.Images != null && dto.Images.Count > 0)
            {
                foreach (var file in dto.Images)
                {
                    var fileUrl = await SaveImageAsync(file);
                   
                    imageUrls.Add($"{baseUrl}{fileUrl}");
                }

            }

        


            //// Generate unique file name
            //var fileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.Image?.FileName);

            //// Define the path to save the image
            //var filePath = Path.Combine(_env.WebRootPath, "Image", "Game", fileName);

            //// Ensure the directory exists
            //if (!Directory.Exists(Path.Combine(_env.WebRootPath, "Image", "Game")))
            //{
            //    Directory.CreateDirectory(Path.Combine(_env.WebRootPath, "Image", "Game"));
            //}

            //// Save the file
            //using (var stream = new FileStream(filePath, FileMode.Create))
            //{
            //    await dto.Image.CopyToAsync(stream);
            //}


            dto.ImageUrl = imageUrl;
            dto.ImageUrls = imageUrls;




            var game =  _mapper.Map<Game>(dto);
            var newGame = await _rep.Create(game);
            await _rep.Create(game);
            await _rep.SaveChangesAsync();

            return _mapper.Map<GetGameDto>(newGame);


        }

        private async Task<string> SaveImageAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new Exception("Invalid image file.");

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(_env.WebRootPath, "Image", "Game", fileName);

            // Ensure the directory exists
            if (!Directory.Exists(Path.Combine(_env.WebRootPath, "Image", "Game")))
            {
                Directory.CreateDirectory(Path.Combine(_env.WebRootPath, "Image", "Game"));
            }

            // Save the file
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }


            return $"/Image/Game/{fileName}";
        }


        public async Task Delete(int id)
        {
            var game = await _rep.GetById(id);
            _rep.Delete(_mapper.Map<Game>(game));
            await _rep.SaveChangesAsync();
        }

        public List<GetGameDto> GetAll()
        {
            List<GetGameDto> dtos = new();
            var datas = _rep.GetAll();
            foreach (var data in datas)
            {
                GetGameDto dto = _mapper.Map<GetGameDto>(data);
                dtos.Add(dto);

            }
            return dtos;
        }

        public async Task<GetGameDto> GetById(int id)
        {
       
            var dto = _mapper.Map<GetGameDto>(await _rep.GetById(id));
            return dto;
        }

        public async Task SoftDelete(int id)
        {
            var game = await GetById(id);
            _rep.Delete(_mapper.Map<Game>(game));
            await _rep.SaveChangesAsync();
        }

        public async Task Update(UpdateGameDto dto, string baseUrl)
        {
            string imageUrl = null;
            List<string> imageUrls = new List<string>();


            // ✅ Handle Single Image Upload
            if (dto.Image != null)
            {
                string fileUrl = await SaveImageAsync(dto.Image);
                imageUrl = $"{baseUrl}{fileUrl}"; // ✅ Return full URL
            }

            // ✅ Handle Multiple Image Uploads
            if (dto.Images != null && dto.Images.Count > 0)
            {
                foreach (var file in dto.Images)
                {
                    var fileUrl = await SaveImageAsync(file);

                    imageUrls.Add($"{baseUrl}{fileUrl}");
                }

            }


            dto.ImageUrl = imageUrl;
            dto.ImageUrls = imageUrls;


            var oldGame = await GetById(dto.Id);



            oldGame = _mapper.Map<GetGameDto>(dto);
            _rep.Update(_mapper.Map<Game>(oldGame));
            await _rep.SaveChangesAsync();
        }
    }
}
