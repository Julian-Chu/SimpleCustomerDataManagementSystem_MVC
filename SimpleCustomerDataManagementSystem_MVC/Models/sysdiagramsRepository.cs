using System;
using System.Linq;
using System.Collections.Generic;
	
namespace SimpleCustomerDataManagementSystem_MVC.Models
{   
	public  class sysdiagramsRepository : EFRepository<sysdiagrams>, IsysdiagramsRepository
	{

	}

	public  interface IsysdiagramsRepository : IRepository<sysdiagrams>
	{

	}
}