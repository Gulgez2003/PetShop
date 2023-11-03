namespace Business.Services.Concrete
{
    public class TestimonialService : ITestimonialService
    {
        private readonly ITestimonialRepository _testimonialRepository;
        private readonly IMapper _mapper;
        public TestimonialService(ITestimonialRepository testimonialRepository, IMapper mapper)
        {
            _testimonialRepository = testimonialRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(TestimonialPostDTO postDto)
        {
            await _testimonialRepository.CreateAsync(_mapper.Map<Testimonial>(postDto));
            await _testimonialRepository.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Testimonial testimonial = await _testimonialRepository.GetAsync(t => t.Id == id && !t.IsDeleted);
            if (testimonial == null)
            {
                throw new NotFoundException(Messages.TestimonialNotFound);
            }
            _testimonialRepository.Delete(testimonial);
            await _testimonialRepository.SaveAsync();
        }

        public async Task ApproveAsync(int id)
        {
            Testimonial testimonial = await _testimonialRepository.GetAsync(t => t.Id == id && !t.IsDeleted);
             if (testimonial == null)
            {
                throw new NotFoundException(Messages.TestimonialNotFound);
            }

            testimonial.IsApproved = true;
            testimonial.IsChecked = true;
            _testimonialRepository.Update(testimonial);
            await _testimonialRepository.SaveAsync();
        }

        public async Task DisapproveAsync(int id)
        {
            Testimonial testimonial = await _testimonialRepository.GetAsync(t => t.Id == id && !t.IsDeleted);
            if (testimonial == null)
            {
                throw new NotFoundException(Messages.TestimonialNotFound);
            }

            testimonial.IsApproved = false;
            testimonial.IsChecked = true;
            _testimonialRepository.Update(testimonial);
            await _testimonialRepository.SaveAsync();
        }

        public async Task<List<TestimonialGetDTO>> GetAllAsync()
        {
            List<Testimonial> testimonials = await _testimonialRepository.GetAllAsync(t => !t.IsDeleted && !t.IsChecked);
            
            return _mapper.Map<List<TestimonialGetDTO>>(testimonials);
        }

        public async Task<List<TestimonialGetDTO>> GetAllIncludingAsync()
        {
            Expression<Func<Testimonial, object>>[] includes = { t => t.Product };
            List<Testimonial> testimonials = _testimonialRepository.GetAllIncluding(includes).ToList();

            if (!testimonials.Any())
            {
                throw new NotFoundException(Messages.TestimonialNotFound);
            }

            return _mapper.Map<List<TestimonialGetDTO>>(testimonials);
        }

        public async Task<List<TestimonialGetDTO>> GetPublishedByIdAsync(int id)  
        {
            List<Testimonial> testimonials = await _testimonialRepository.GetAllAsync(t => !t.IsDeleted && t.IsApproved && t.ProductId == id);
           
            return _mapper.Map<List<TestimonialGetDTO>>(testimonials);
        }

        public async Task<List<TestimonialGetDTO>> GetAllPublishedAsync()
        {
            List<Testimonial> testimonials = await _testimonialRepository.GetAllAsync(t => !t.IsDeleted && t.IsApproved);

            return _mapper.Map<List<TestimonialGetDTO>>(testimonials);
        }

        public async Task<List<TestimonialGetDTO>> GetAllRejectedAsync()
        {
            List<Testimonial> testimonials = await _testimonialRepository.GetAllAsync(t => !t.IsDeleted && !t.IsApproved);
           
            return _mapper.Map<List<TestimonialGetDTO>>(testimonials);
        }

        public async Task<TestimonialGetDTO> GetByIdAsync(int id)
        {
            Testimonial testimonial = await _testimonialRepository.GetAsync(t => t.Id == id && !t.IsDeleted);
            if (testimonial == null)
            {
                throw new NotFoundException(Messages.TestimonialNotFound);
            }
            return _mapper.Map<TestimonialGetDTO>(testimonial);
        }

        public async Task UpdateAsync(TestimonialUpdateDTO updateDto)
        {
            Testimonial testimonial = await _testimonialRepository.GetAsync(t => t.Id == updateDto.testimonialGetDTO.Id && !t.IsDeleted);
            if (testimonial == null)
            {
                throw new NotFoundException(Messages.TestimonialNotFound);
            }
            testimonial.Comment = updateDto.testimonialPostDTO.Comment;
            testimonial.FullName = updateDto.testimonialPostDTO.FullName;
            testimonial.Email = updateDto.testimonialPostDTO.Email;


            _testimonialRepository.Update(testimonial);
            await _testimonialRepository.SaveAsync();
        }
    }
}
