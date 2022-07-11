using Core2_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Core2_Api.Infra
{
	public abstract class BaseAppUser<TKey>
	{
		public TKey Id { get; set; }

		private readonly AppDbContext _context;
		public BaseAppUser(AppDbContext context)
		{
			_context = context;
		
		}

		//public IActionResult Test(IEntity entity, CancellationToken cancellationToken)
		//{
		//	var collection = _context.Entry(entity).Collection("");

		//}
	}

	public interface IEntity
	{
	}

	public abstract class BaseAppUser : BaseAppUser<int>
	{
		public BaseAppUser(AppDbContext context) : base(context)
		{
		}
	}


	public class CustomeApiResultAttribute : ActionFilterAttribute
	{
		public override void OnResultExecuting(ResultExecutingContext context)
		{

			if (context.Result is BadRequestObjectResult badRequestObjectResult)
			{
				if(badRequestObjectResult.Value is SerializableError errors)
				{

				}
			}
			//base.OnResultExecuting(context);
		}
	}

	//[BindRequired]
	//[Table(name:"",Schema ="")]
	public class Model1
	{
		//[BindRequired]
		//[BindNever]
		//[Column("DbTest_Id",TypeName ="nVarChar",Order =2)]
		//[MaxLength(128)]
		//[Key]
		public int Id { get; set; }

		//[ForeignKey("")]
		public int Name { get; set; }
		//public DateTime? BirthDate { get; set; }

		public Model2 Models2s { get; set; }

		//[ForeignKey(nameof(Models2s))]
		public int FK_Model2 { get; set; }
	}

	public class Model2
	{

		public int Id { get; set; }
		public Model1 Model1s { get; set; }

		public int Fk_Model1 { get; set; }

	}
	
}
