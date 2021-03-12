using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000B11 RID: 2833
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidMailboxProvisioningConstraintException : DataSourceOperationException
	{
		// Token: 0x06008214 RID: 33300 RVA: 0x001A7C71 File Offset: 0x001A5E71
		public InvalidMailboxProvisioningConstraintException(string parserErrorString) : base(DirectoryStrings.ErrorInvalidMailboxProvisioningConstraint(parserErrorString))
		{
			this.parserErrorString = parserErrorString;
		}

		// Token: 0x06008215 RID: 33301 RVA: 0x001A7C86 File Offset: 0x001A5E86
		public InvalidMailboxProvisioningConstraintException(string parserErrorString, Exception innerException) : base(DirectoryStrings.ErrorInvalidMailboxProvisioningConstraint(parserErrorString), innerException)
		{
			this.parserErrorString = parserErrorString;
		}

		// Token: 0x06008216 RID: 33302 RVA: 0x001A7C9C File Offset: 0x001A5E9C
		protected InvalidMailboxProvisioningConstraintException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.parserErrorString = (string)info.GetValue("parserErrorString", typeof(string));
		}

		// Token: 0x06008217 RID: 33303 RVA: 0x001A7CC6 File Offset: 0x001A5EC6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("parserErrorString", this.parserErrorString);
		}

		// Token: 0x17002F27 RID: 12071
		// (get) Token: 0x06008218 RID: 33304 RVA: 0x001A7CE1 File Offset: 0x001A5EE1
		public string ParserErrorString
		{
			get
			{
				return this.parserErrorString;
			}
		}

		// Token: 0x04005601 RID: 22017
		private readonly string parserErrorString;
	}
}
