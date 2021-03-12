using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x0200038C RID: 908
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "BindingRedirectionException", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[DebuggerStepThrough]
	public class BindingRedirectionException : MsolAdministrationException
	{
		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06001668 RID: 5736 RVA: 0x0008C00B File Offset: 0x0008A20B
		// (set) Token: 0x06001669 RID: 5737 RVA: 0x0008C013 File Offset: 0x0008A213
		[DataMember]
		public string[] Locations
		{
			get
			{
				return this.LocationsField;
			}
			set
			{
				this.LocationsField = value;
			}
		}

		// Token: 0x04001009 RID: 4105
		private string[] LocationsField;
	}
}
