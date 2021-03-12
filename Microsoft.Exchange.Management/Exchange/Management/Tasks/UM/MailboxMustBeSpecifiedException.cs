using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02001203 RID: 4611
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MailboxMustBeSpecifiedException : LocalizedException
	{
		// Token: 0x0600B9E5 RID: 47589 RVA: 0x002A68CB File Offset: 0x002A4ACB
		public MailboxMustBeSpecifiedException(string parameter) : base(Strings.MailboxMustBeSpecifiedException(parameter))
		{
			this.parameter = parameter;
		}

		// Token: 0x0600B9E6 RID: 47590 RVA: 0x002A68E0 File Offset: 0x002A4AE0
		public MailboxMustBeSpecifiedException(string parameter, Exception innerException) : base(Strings.MailboxMustBeSpecifiedException(parameter), innerException)
		{
			this.parameter = parameter;
		}

		// Token: 0x0600B9E7 RID: 47591 RVA: 0x002A68F6 File Offset: 0x002A4AF6
		protected MailboxMustBeSpecifiedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.parameter = (string)info.GetValue("parameter", typeof(string));
		}

		// Token: 0x0600B9E8 RID: 47592 RVA: 0x002A6920 File Offset: 0x002A4B20
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("parameter", this.parameter);
		}

		// Token: 0x17003A56 RID: 14934
		// (get) Token: 0x0600B9E9 RID: 47593 RVA: 0x002A693B File Offset: 0x002A4B3B
		public string Parameter
		{
			get
			{
				return this.parameter;
			}
		}

		// Token: 0x04006471 RID: 25713
		private readonly string parameter;
	}
}
