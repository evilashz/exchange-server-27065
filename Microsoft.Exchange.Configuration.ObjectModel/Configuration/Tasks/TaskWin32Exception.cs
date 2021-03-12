using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020000C9 RID: 201
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TaskWin32Exception : LocalizedException
	{
		// Token: 0x06000784 RID: 1924 RVA: 0x0001BB0B File Offset: 0x00019D0B
		public TaskWin32Exception(Win32Exception e) : this((e != null) ? Strings.ErrorTaskWin32Exception(e.Message) : Strings.ErrorTaskWin32Exception(string.Empty), e)
		{
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x0001BB2E File Offset: 0x00019D2E
		public TaskWin32Exception(LocalizedString message, Win32Exception e) : base(message, e)
		{
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x0001BB38 File Offset: 0x00019D38
		protected TaskWin32Exception(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x0001BB44 File Offset: 0x00019D44
		public static TaskWin32Exception FromErrorCodeAndVerbose(int error, LocalizedString verbose)
		{
			Win32Exception ex = new Win32Exception(error);
			return new TaskWin32Exception(Strings.ErrorTaskWin32ExceptionVerbose(ex.Message, verbose), ex);
		}
	}
}
