﻿namespace AngularServiceStackMongo.ServiceModel.Type
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using AngularServiceStackMongo.Core;
    using AngularServiceStackMongo.Domain;
    using ServiceStack;
    using ServiceStack.FluentValidation;
    using ServiceStack.FluentValidation.Results;

    public class TimelineValidator : AbstractValidator<ServiceModel.Timeline>
    {
        public TimelineValidator(TimelineRepository repository)
        {
            RuleFor(b => b.Name).NotEmpty();

            RuleSet(ApplyTo.Post, () =>
            {
                RuleFor(b => b.Id.Trim()).Equal(string.Empty);

                Custom(b =>
                {
                    if (repository.GetByName(b.Name).Any())
                    {
                        return new ValidationFailure("Name", "A timeline with the same name already exists", "Duplicate", b.Name);
                    }
                    return null;
                });

                Custom(b =>
                {
                    if (repository.GetByName(b.Name).Count() >= 10) //TODO: Remove once server side paging is implemented
                    {
                        return new ValidationFailure("", "You can only upload a set number of timelines", "Max number of uploads");
                    }
                    return null;
                });
            });
        }
    }

    public class BlankValidator : AbstractValidator<ServiceModel.Timeline>
    {
        public BlankValidator()
        {

        }
    }
}
