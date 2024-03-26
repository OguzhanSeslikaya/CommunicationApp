using Communication.Entity.ViewModels.Groups;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validation.GroupValidations
{
    public class CreateGroupValidation : AbstractValidator<CreateGroupVM>
    {
        public CreateGroupValidation()
        {
            RuleFor(p => p.groupName)
                .NotNull()
                    .WithMessage("Groupname cannot be blank.")
                .MinimumLength(5)
                    .WithMessage("Groupname must be a minimum of 5 characters.")
                .MaximumLength(15)
                    .WithMessage("Groupname must be maximum 15 characters long.")
                .Must(x =>
                {
                    string ozelKarakterler = ",_?=)(/&%+^'!é\\<>£#$½{[]}\"|@*-.";
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
                    .WithMessage("Groupname cannot contain special characters.")
                .Must(ad => {
                    if (ad != null)
                    {
                        return !ad.Contains(" ");
                    }
                    return true;
                })
                    .WithMessage("Groupname cannot contain spaces.");
        }
    }
}
