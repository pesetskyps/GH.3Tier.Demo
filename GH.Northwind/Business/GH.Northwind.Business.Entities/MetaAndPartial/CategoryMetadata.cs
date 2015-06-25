using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using GH.Common.Framework.Business;

namespace GH.Northwind.Business.Entities
{
    [MetadataType(typeof(CategoryMetadata))]
    public partial class Category : BusinessEntityBase
    {
    }

    public class CategoryMetadata
    {
        //Oops, the metadata of this class hasn't been auto-generated yet, generate it by youself if you need
    }
}