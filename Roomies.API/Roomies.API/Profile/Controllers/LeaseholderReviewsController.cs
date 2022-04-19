using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Roomies.API.Domain.Models;
using Roomies.API.Domain.Services;
using Roomies.API.Resources;

namespace Roomies.API.Controllers
{
    [Route("/api/leaseholders/{leaseholderId}/reviews")]
    public class LeaseholderReviewsController
    {
        private IReviewService _reviewService;
        private readonly IMapper _mapper;

        public LeaseholderReviewsController(IReviewService reviewService, IMapper mapper)
        {
            _reviewService = reviewService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ReviewResource>> GetAllByLeaseholderIdAsync(int leaseholderId)
        {
            var reviews = await _reviewService.ListByLeaseholderIdAsync(leaseholderId);
            var resources = _mapper.Map<IEnumerable<Review>, IEnumerable<ReviewResource>>(reviews);

            return resources;
        }
    }
}
