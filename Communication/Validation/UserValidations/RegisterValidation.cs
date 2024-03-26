using Communication.Entity.ViewModels.User;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validation.UserValidations
{
    public class RegisterValidation : AbstractValidator<RegisterVM>
    {
        public RegisterValidation()
        {
            RuleFor(p => p.username)
                .NotNull()
                    .WithMessage("Username cannot be blank.")
                .MinimumLength(5)
                    .WithMessage("Username must be a minimum of 5 characters.")
                .MaximumLength(15)
                    .WithMessage("Username must be maximum 15 characters long.")
                .Must(x =>
                {
                    string ozelKarakterler = ",_?=)(/&%+^'!é\\<>£#$½{[]}\"|@*";
                    foreach (var item in ozelKarakterler)
                    {
                        if (x != null)
                        {
                            foreach (var item2 in x)
                            {
                                if (item.Equals(item2))
                                {
                                    return false;
                                }
                            }
                        }
                    }
                    return true;
                })
                    .WithMessage("Username cannot contain special characters except - and .")
                .Must(ad => { 
                    if (ad != null)
                    {
                        return !ad.Contains(" ");
                    }
                    return true;
                })
                    .WithMessage("Username cannot contain spaces.");




            RuleFor(p => p.password)
                .NotNull()
                    .WithMessage("Password cannot be blank.")
                .MinimumLength(6)
                    .WithMessage("Password must be at least 6 characters long.")
                .MaximumLength(30)
                    .WithMessage("The password can be up to 30 characters long.")
                .Must(p => {
                    if (p != null)
                    {
                        return !p.Contains(" ");
                    }
                    return true;
                })
                    .WithMessage("Password cannot contain spaces.")
                .Must(x =>
                {
                    string numbers = "1234567890";
                    foreach (var item in numbers)
                    {
                        if(x != null) 
                        { 
                            foreach (var item2 in x)
                            {
                                if (item.Equals(item2))
                                {
                                    return true;
                                }
                            }
                        }
                    }
                    return false;
                })
                    .WithMessage("The password must contain at least one digit.")
                .Must(x =>
                {
                    string uppercaseLetters = "ABCDEFGHİJKLMNOPRSTUVYZWXÇĞİÖÜ";
                    foreach (var item in uppercaseLetters)
                    {
                        if(x != null) 
                        { 
                            foreach (var item2 in x)
                            {
                                if (item.Equals(item2))
                                {
                                    return true;
                                }
                            }
                        }
                    }
                    return false;
                })
                    .WithMessage("Password must contain at least one uppercase letter.")
                .Must(x =>
                {
                    string lowercaseLetters = "abcdefghijklmnoprsştuvyzwxçğiöü";
                    foreach (var item in lowercaseLetters)
                    {
                        if(x != null) 
                        { 
                            foreach (var item2 in x)
                            {
                                if (item.Equals(item2))
                                {
                                    return true;
                                }
                            }
                        }
                    }
                    return false;
                })
                    .WithMessage("The password must contain at least one lowercase letter.")
                .Must(x =>
                {
                    string ozelKarakterler = "!^+%&()=?_|][½#£><é-,@.*";
                    foreach (var item in ozelKarakterler)
                    {
                        if (x != null) 
                        { 
                            foreach (var item2 in x)
                            {
                                if (item.Equals(item2))
                                {
                                    return true;
                                }
                            }
                        }
                    }
                    return false;
                })
                    .WithMessage("The password must contain at least one special character.")
                .Must(x =>
                {
                    string ozelKarakterler = "'}\"/\\(){$";
                    foreach (var item in ozelKarakterler)
                    {
                        if(x != null) 
                        { 
                            foreach (var item2 in x)
                            {
                                if (item.Equals(item2))
                                {
                                    return false;
                                }
                            }
                        }
                    }
                    return true;
                })
                    .WithMessage("Password cannot contain the characters '\"/(){}$\\");
        }
    }
}
