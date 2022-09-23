using Application.Features.UserSocialMediaAddresses.Constants;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserSocialMediaAddresses.Rules
{
    public class UserSocialMediaAddressBusinessRule
    {
        private readonly IUserSocialMediaAddressRepository _userSocialMediaAddressRepository;
        private readonly IUserRepository _userRepository;

        public UserSocialMediaAddressBusinessRule(IUserSocialMediaAddressRepository userSocialMediaAddressRepository, IUserRepository userRepository)
        {
            _userSocialMediaAddressRepository = userSocialMediaAddressRepository;
            _userRepository = userRepository;
        }

        public async Task UserSocialMediaAddressGithubUrlCanNotBeDuplicated(string requestGithubUrl)
        {
            var userSocialMediaAddress = await _userSocialMediaAddressRepository.GetAsync(x => x.GithubUrl == requestGithubUrl);

            if (userSocialMediaAddress != null)
                throw new BusinessException(UserSocialMediaAddressMessages.GithubUrlCanNotBeDuplicated);
        }

        public async Task UserMustBeExist(int requestUserId)
        {
            var user = await _userRepository.GetAsync(x => x.Id == requestUserId);

            if (user is null)
                throw new BusinessException(UserSocialMediaAddressMessages.UserNotFound);
        }

        public void SocialMediaAddressShouldExistWhenRequested(UserSocialMediaAddress? userSocialMediaAddress)
        {
            if (userSocialMediaAddress is null)
                throw new BusinessException(UserSocialMediaAddressMessages.UserSocialMediaAddressIsNotFound);
        }

        public async Task UserSocialMediaAddressCanNotHaveMoreThanOneGithubAddress(int requestUserId)
        {
            var userSocialMediaAddress = await _userSocialMediaAddressRepository.GetListAsync(x => x.UserId == requestUserId);
            var socialMediaAddressCount = userSocialMediaAddress.Items.Select(x => x.GithubUrl).Count();
            if (socialMediaAddressCount > 1)
                throw new BusinessException(UserSocialMediaAddressMessages.CanNotAddMultipleGithubAddresses);
        }
    }
}
