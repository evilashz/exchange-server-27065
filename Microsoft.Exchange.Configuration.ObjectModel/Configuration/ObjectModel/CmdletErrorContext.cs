using System;

namespace Microsoft.Exchange.Configuration.ObjectModel
{
	// Token: 0x02000003 RID: 3
	internal class CmdletErrorContext
	{
		// Token: 0x06000010 RID: 16 RVA: 0x000024E8 File Offset: 0x000006E8
		internal CmdletErrorContext(Type exceptionType) : this(exceptionType, null, null)
		{
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000024F3 File Offset: 0x000006F3
		internal CmdletErrorContext(Type exceptionType, Type innerExceptionType) : this(exceptionType, innerExceptionType, null)
		{
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000024FE File Offset: 0x000006FE
		internal CmdletErrorContext(Type exceptionType, Type innerExceptionType, string hostName)
		{
			if (exceptionType == null)
			{
				throw new ArgumentNullException("exceptionType");
			}
			this.exceptionType = exceptionType;
			this.innerExceptionType = innerExceptionType;
			this.hostName = hostName;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000013 RID: 19 RVA: 0x0000252F File Offset: 0x0000072F
		internal Type ExceptionType
		{
			get
			{
				return this.exceptionType;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002537 File Offset: 0x00000737
		internal Type InnerExceptionType
		{
			get
			{
				return this.innerExceptionType;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000015 RID: 21 RVA: 0x0000253F File Offset: 0x0000073F
		internal string HostName
		{
			get
			{
				return this.hostName;
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002548 File Offset: 0x00000748
		internal bool MatchesErrorContext(Exception exception, string hostName)
		{
			return this.exceptionType.IsInstanceOfType(exception) && (!(this.innerExceptionType != null) || exception.InnerException == null || this.innerExceptionType.IsInstanceOfType(exception.InnerException)) && (this.HostName == null || this.HostName.Equals(hostName));
		}

		// Token: 0x04000008 RID: 8
		private Type exceptionType;

		// Token: 0x04000009 RID: 9
		private Type innerExceptionType;

		// Token: 0x0400000A RID: 10
		private string hostName;
	}
}
