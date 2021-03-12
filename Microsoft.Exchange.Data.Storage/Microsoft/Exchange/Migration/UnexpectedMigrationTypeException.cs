using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020001A1 RID: 417
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class UnexpectedMigrationTypeException : MigrationPermanentException
	{
		// Token: 0x06001777 RID: 6007 RVA: 0x00070BA5 File Offset: 0x0006EDA5
		public UnexpectedMigrationTypeException(string discoveredType, string expectedType) : base(Strings.ErrorUnexpectedMigrationType(discoveredType, expectedType))
		{
			this.discoveredType = discoveredType;
			this.expectedType = expectedType;
		}

		// Token: 0x06001778 RID: 6008 RVA: 0x00070BC2 File Offset: 0x0006EDC2
		public UnexpectedMigrationTypeException(string discoveredType, string expectedType, Exception innerException) : base(Strings.ErrorUnexpectedMigrationType(discoveredType, expectedType), innerException)
		{
			this.discoveredType = discoveredType;
			this.expectedType = expectedType;
		}

		// Token: 0x06001779 RID: 6009 RVA: 0x00070BE0 File Offset: 0x0006EDE0
		protected UnexpectedMigrationTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.discoveredType = (string)info.GetValue("discoveredType", typeof(string));
			this.expectedType = (string)info.GetValue("expectedType", typeof(string));
		}

		// Token: 0x0600177A RID: 6010 RVA: 0x00070C35 File Offset: 0x0006EE35
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("discoveredType", this.discoveredType);
			info.AddValue("expectedType", this.expectedType);
		}

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x0600177B RID: 6011 RVA: 0x00070C61 File Offset: 0x0006EE61
		public string DiscoveredType
		{
			get
			{
				return this.discoveredType;
			}
		}

		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x0600177C RID: 6012 RVA: 0x00070C69 File Offset: 0x0006EE69
		public string ExpectedType
		{
			get
			{
				return this.expectedType;
			}
		}

		// Token: 0x04000B1B RID: 2843
		private readonly string discoveredType;

		// Token: 0x04000B1C RID: 2844
		private readonly string expectedType;
	}
}
