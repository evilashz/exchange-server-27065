using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001033 RID: 4147
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MandatoryParameterException : LocalizedException
	{
		// Token: 0x0600AFB6 RID: 44982 RVA: 0x00294C11 File Offset: 0x00292E11
		public MandatoryParameterException(string parameter) : base(Strings.ExceptionMandatoryParameter(parameter))
		{
			this.parameter = parameter;
		}

		// Token: 0x0600AFB7 RID: 44983 RVA: 0x00294C26 File Offset: 0x00292E26
		public MandatoryParameterException(string parameter, Exception innerException) : base(Strings.ExceptionMandatoryParameter(parameter), innerException)
		{
			this.parameter = parameter;
		}

		// Token: 0x0600AFB8 RID: 44984 RVA: 0x00294C3C File Offset: 0x00292E3C
		protected MandatoryParameterException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.parameter = (string)info.GetValue("parameter", typeof(string));
		}

		// Token: 0x0600AFB9 RID: 44985 RVA: 0x00294C66 File Offset: 0x00292E66
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("parameter", this.parameter);
		}

		// Token: 0x17003807 RID: 14343
		// (get) Token: 0x0600AFBA RID: 44986 RVA: 0x00294C81 File Offset: 0x00292E81
		public string Parameter
		{
			get
			{
				return this.parameter;
			}
		}

		// Token: 0x0400616D RID: 24941
		private readonly string parameter;
	}
}
