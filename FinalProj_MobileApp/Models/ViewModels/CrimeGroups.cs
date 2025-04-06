using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProj_MobileApp.Models.ViewModels
{
	public class CrimeGroups : List<CrimeNotification>
	{
		public string GroupName { get; set; }
		public CrimeGroups(string groupName, List<CrimeNotification> Crimes) : base(Crimes)
		{
			GroupName = groupName;
		}
	}
}
