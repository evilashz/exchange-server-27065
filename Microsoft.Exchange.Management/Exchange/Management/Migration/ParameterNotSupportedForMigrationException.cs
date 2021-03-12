using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Migration
{
	// Token: 0x02001126 RID: 4390
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ParameterNotSupportedForMigrationException : LocalizedException
	{
		// Token: 0x0600B4AD RID: 46253 RVA: 0x0029D2A1 File Offset: 0x0029B4A1
		public ParameterNotSupportedForMigrationException(string parameterName) : base(Strings.ErrorParameterNotSupportedForMigration(parameterName))
		{
			this.parameterName = parameterName;
		}

		// Token: 0x0600B4AE RID: 46254 RVA: 0x0029D2B6 File Offset: 0x0029B4B6
		public ParameterNotSupportedForMigrationException(string parameterName, Exception innerException) : base(Strings.ErrorParameterNotSupportedForMigration(parameterName), innerException)
		{
			this.parameterName = parameterName;
		}

		// Token: 0x0600B4AF RID: 46255 RVA: 0x0029D2CC File Offset: 0x0029B4CC
		protected ParameterNotSupportedForMigrationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.parameterName = (string)info.GetValue("parameterName", typeof(string));
		}

		// Token: 0x0600B4B0 RID: 46256 RVA: 0x0029D2F6 File Offset: 0x0029B4F6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("parameterName", this.parameterName);
		}

		// Token: 0x17003932 RID: 14642
		// (get) Token: 0x0600B4B1 RID: 46257 RVA: 0x0029D311 File Offset: 0x0029B511
		public string ParameterName
		{
			get
			{
				return this.parameterName;
			}
		}

		// Token: 0x04006298 RID: 25240
		private readonly string parameterName;
	}
}
