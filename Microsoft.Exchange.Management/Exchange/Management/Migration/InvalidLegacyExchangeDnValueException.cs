using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Migration
{
	// Token: 0x0200110E RID: 4366
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidLegacyExchangeDnValueException : LocalizedException
	{
		// Token: 0x0600B436 RID: 46134 RVA: 0x0029C743 File Offset: 0x0029A943
		public InvalidLegacyExchangeDnValueException(string parameterName) : base(Strings.InvalidLegacyExchangeDnParameterValue(parameterName))
		{
			this.parameterName = parameterName;
		}

		// Token: 0x0600B437 RID: 46135 RVA: 0x0029C758 File Offset: 0x0029A958
		public InvalidLegacyExchangeDnValueException(string parameterName, Exception innerException) : base(Strings.InvalidLegacyExchangeDnParameterValue(parameterName), innerException)
		{
			this.parameterName = parameterName;
		}

		// Token: 0x0600B438 RID: 46136 RVA: 0x0029C76E File Offset: 0x0029A96E
		protected InvalidLegacyExchangeDnValueException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.parameterName = (string)info.GetValue("parameterName", typeof(string));
		}

		// Token: 0x0600B439 RID: 46137 RVA: 0x0029C798 File Offset: 0x0029A998
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("parameterName", this.parameterName);
		}

		// Token: 0x1700391B RID: 14619
		// (get) Token: 0x0600B43A RID: 46138 RVA: 0x0029C7B3 File Offset: 0x0029A9B3
		public string ParameterName
		{
			get
			{
				return this.parameterName;
			}
		}

		// Token: 0x04006281 RID: 25217
		private readonly string parameterName;
	}
}
