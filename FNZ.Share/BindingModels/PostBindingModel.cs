using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using FNZ.Share.Models;
using Microsoft.AspNetCore.Http;

namespace FNZ.Share.BindingModels
{
    public class PostBindingModel
    {
        [Required]
        public string Author { get; set; }
        [Required]
        public Enums.Category Category { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string Title { get; set; }
        [RequiredIf("Category", Enums.Category.DogsAdoption, Enums.Category.CatsAdoption)]
        public string Age { get; set; }
        [RequiredIf("Category", Enums.Category.DogsAdoption, Enums.Category.CatsAdoption)]
        public string Breed { get; set; }
        [RequiredIf("Category", Enums.Category.DogsAdoption, Enums.Category.CatsAdoption)]
        public string MaxWeight { get; set; }
        [RequiredIf("Category", Enums.Category.DogsAdoption, Enums.Category.CatsAdoption)]
        public string Name { get; set; }



        public IFormFile File { get; set; }
        //public string PhotoPath { get; set; }

        public class RequiredIfAttribute : ValidationAttribute
        {
            RequiredAttribute _innerAttribute = new RequiredAttribute();
            public string _dependentProperty { get; set; }
            public object[] _targetValue { get; set; }

            public RequiredIfAttribute(string dependentProperty, params object[] targetValue)
            {
                this._dependentProperty = dependentProperty;
                this._targetValue = targetValue;
            }
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                var field = validationContext.ObjectType.GetProperty(_dependentProperty);
                if (field != null)
                {
                    var dependentValue = field.GetValue(validationContext.ObjectInstance, null);
                    foreach (var obj in _targetValue)
                    {
                        if ((dependentValue == null && this._targetValue == null) || (dependentValue != null && dependentValue.Equals(obj)))
                        {
                            if (!_innerAttribute.IsValid(value))
                            {
                                string name = validationContext.DisplayName;
                                return new ValidationResult(ErrorMessage = name + " Is required.");
                            }
                        }
                    }
                    
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult(FormatErrorMessage(_dependentProperty));
                }
            }
        }
    }
}
