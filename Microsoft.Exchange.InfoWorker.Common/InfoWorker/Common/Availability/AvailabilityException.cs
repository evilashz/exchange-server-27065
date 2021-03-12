using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x02000004 RID: 4
	internal abstract class AvailabilityException : LocalizedException
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002297 File Offset: 0x00000497
		// (set) Token: 0x06000005 RID: 5 RVA: 0x0000229F File Offset: 0x0000049F
		public string ServerName { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000022A8 File Offset: 0x000004A8
		// (set) Token: 0x06000007 RID: 7 RVA: 0x000022B0 File Offset: 0x000004B0
		public string LocationIdentifier { get; set; }

		// Token: 0x06000008 RID: 8 RVA: 0x000022B9 File Offset: 0x000004B9
		public AvailabilityException(ErrorConstants errorCode, LocalizedString localizedString) : base(localizedString)
		{
			base.ErrorCode = (int)errorCode;
			this.ServerName = ExceptionDefaults.DefaultMachineName;
			this.LocationIdentifier = string.Empty;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000022DF File Offset: 0x000004DF
		public AvailabilityException(ErrorConstants errorCode, LocalizedString localizedString, uint locationIdentifier) : base(localizedString)
		{
			base.ErrorCode = (int)errorCode;
			this.ServerName = ExceptionDefaults.DefaultMachineName;
			this.LocationIdentifier = locationIdentifier.ToString();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002307 File Offset: 0x00000507
		public AvailabilityException(ErrorConstants errorCode, LocalizedString localizedString, Exception innerException) : base(localizedString, innerException)
		{
			base.ErrorCode = (int)errorCode;
			this.ServerName = ExceptionDefaults.DefaultMachineName;
			this.LocationIdentifier = string.Empty;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000232E File Offset: 0x0000052E
		public AvailabilityException(ErrorConstants errorCode, LocalizedString localizedString, Exception innerException, uint locationIdentifier) : base(localizedString, innerException)
		{
			base.ErrorCode = (int)errorCode;
			this.ServerName = ExceptionDefaults.DefaultMachineName;
			this.LocationIdentifier = locationIdentifier.ToString();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002357 File Offset: 0x00000557
		public AvailabilityException(string serverName, ErrorConstants errorCode, LocalizedString localizedString) : base(localizedString)
		{
			base.ErrorCode = (int)errorCode;
			this.ServerName = serverName;
			this.LocationIdentifier = string.Empty;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002379 File Offset: 0x00000579
		protected AvailabilityException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.ServerName = (string)info.GetValue("ServerName", typeof(LocalizedString));
			this.LocationIdentifier = string.Empty;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000023AE File Offset: 0x000005AE
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ServerName", this.ServerName);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000023CC File Offset: 0x000005CC
		public override string ToString()
		{
			if (string.IsNullOrEmpty(this.LocationIdentifier))
			{
				return string.Format("{0}{1}. Name of the server where exception originated: {2}", base.ToString(), Environment.NewLine, this.ServerName);
			}
			return string.Format("{0}{1}. Name of the server where exception originated: {2}. LID: {3}", new object[]
			{
				base.ToString(),
				Environment.NewLine,
				this.ServerName,
				this.LocationIdentifier
			});
		}

		// Token: 0x04000014 RID: 20
		private const string ServerNameKey = "ServerName";
	}
}
