using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Migration
{
	// Token: 0x02001127 RID: 4391
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ParameterNotSupportedForMigrationTypeException : LocalizedException
	{
		// Token: 0x0600B4B2 RID: 46258 RVA: 0x0029D319 File Offset: 0x0029B519
		public ParameterNotSupportedForMigrationTypeException(string parameterName, string migrationType) : base(Strings.ErrorParameterNotSupportedForMigrationType(parameterName, migrationType))
		{
			this.parameterName = parameterName;
			this.migrationType = migrationType;
		}

		// Token: 0x0600B4B3 RID: 46259 RVA: 0x0029D336 File Offset: 0x0029B536
		public ParameterNotSupportedForMigrationTypeException(string parameterName, string migrationType, Exception innerException) : base(Strings.ErrorParameterNotSupportedForMigrationType(parameterName, migrationType), innerException)
		{
			this.parameterName = parameterName;
			this.migrationType = migrationType;
		}

		// Token: 0x0600B4B4 RID: 46260 RVA: 0x0029D354 File Offset: 0x0029B554
		protected ParameterNotSupportedForMigrationTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.parameterName = (string)info.GetValue("parameterName", typeof(string));
			this.migrationType = (string)info.GetValue("migrationType", typeof(string));
		}

		// Token: 0x0600B4B5 RID: 46261 RVA: 0x0029D3A9 File Offset: 0x0029B5A9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("parameterName", this.parameterName);
			info.AddValue("migrationType", this.migrationType);
		}

		// Token: 0x17003933 RID: 14643
		// (get) Token: 0x0600B4B6 RID: 46262 RVA: 0x0029D3D5 File Offset: 0x0029B5D5
		public string ParameterName
		{
			get
			{
				return this.parameterName;
			}
		}

		// Token: 0x17003934 RID: 14644
		// (get) Token: 0x0600B4B7 RID: 46263 RVA: 0x0029D3DD File Offset: 0x0029B5DD
		public string MigrationType
		{
			get
			{
				return this.migrationType;
			}
		}

		// Token: 0x04006299 RID: 25241
		private readonly string parameterName;

		// Token: 0x0400629A RID: 25242
		private readonly string migrationType;
	}
}
