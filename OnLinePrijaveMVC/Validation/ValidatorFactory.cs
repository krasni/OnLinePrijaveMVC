using FluentValidation;
using OnLinePrijaveMVC.Models;
using System;
using System.Collections.Generic;

namespace FluentValidationApplication.Validation
{
    public class ValidatorFactory : ValidatorFactoryBase
    {
        private static Dictionary<Type, IValidator> validators = new Dictionary<Type, IValidator>();

        static ValidatorFactory()
        {
            validators.Add(typeof(IValidator<DistributerVM>), new DistributeriVMValidator());
        }

         public override IValidator CreateInstance(Type validatorType)
        {
            IValidator validator;
            if (validators.TryGetValue(validatorType, out validator))
            {
                return validator;
            }
            return validator;
        }
    }
}