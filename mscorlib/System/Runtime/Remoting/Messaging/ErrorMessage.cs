using System;
using System.Collections;
using System.Reflection;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000844 RID: 2116
	internal class ErrorMessage : IMethodCallMessage, IMethodMessage, IMessage
	{
		// Token: 0x17000F9B RID: 3995
		// (get) Token: 0x06005ACD RID: 23245 RVA: 0x0013DFF9 File Offset: 0x0013C1F9
		public IDictionary Properties
		{
			[SecurityCritical]
			get
			{
				return null;
			}
		}

		// Token: 0x17000F9C RID: 3996
		// (get) Token: 0x06005ACE RID: 23246 RVA: 0x0013DFFC File Offset: 0x0013C1FC
		public string Uri
		{
			[SecurityCritical]
			get
			{
				return this.m_URI;
			}
		}

		// Token: 0x17000F9D RID: 3997
		// (get) Token: 0x06005ACF RID: 23247 RVA: 0x0013E004 File Offset: 0x0013C204
		public string MethodName
		{
			[SecurityCritical]
			get
			{
				return this.m_MethodName;
			}
		}

		// Token: 0x17000F9E RID: 3998
		// (get) Token: 0x06005AD0 RID: 23248 RVA: 0x0013E00C File Offset: 0x0013C20C
		public string TypeName
		{
			[SecurityCritical]
			get
			{
				return this.m_TypeName;
			}
		}

		// Token: 0x17000F9F RID: 3999
		// (get) Token: 0x06005AD1 RID: 23249 RVA: 0x0013E014 File Offset: 0x0013C214
		public object MethodSignature
		{
			[SecurityCritical]
			get
			{
				return this.m_MethodSignature;
			}
		}

		// Token: 0x17000FA0 RID: 4000
		// (get) Token: 0x06005AD2 RID: 23250 RVA: 0x0013E01C File Offset: 0x0013C21C
		public MethodBase MethodBase
		{
			[SecurityCritical]
			get
			{
				return null;
			}
		}

		// Token: 0x17000FA1 RID: 4001
		// (get) Token: 0x06005AD3 RID: 23251 RVA: 0x0013E01F File Offset: 0x0013C21F
		public int ArgCount
		{
			[SecurityCritical]
			get
			{
				return this.m_ArgCount;
			}
		}

		// Token: 0x06005AD4 RID: 23252 RVA: 0x0013E027 File Offset: 0x0013C227
		[SecurityCritical]
		public string GetArgName(int index)
		{
			return this.m_ArgName;
		}

		// Token: 0x06005AD5 RID: 23253 RVA: 0x0013E02F File Offset: 0x0013C22F
		[SecurityCritical]
		public object GetArg(int argNum)
		{
			return null;
		}

		// Token: 0x17000FA2 RID: 4002
		// (get) Token: 0x06005AD6 RID: 23254 RVA: 0x0013E032 File Offset: 0x0013C232
		public object[] Args
		{
			[SecurityCritical]
			get
			{
				return null;
			}
		}

		// Token: 0x17000FA3 RID: 4003
		// (get) Token: 0x06005AD7 RID: 23255 RVA: 0x0013E035 File Offset: 0x0013C235
		public bool HasVarArgs
		{
			[SecurityCritical]
			get
			{
				return false;
			}
		}

		// Token: 0x17000FA4 RID: 4004
		// (get) Token: 0x06005AD8 RID: 23256 RVA: 0x0013E038 File Offset: 0x0013C238
		public int InArgCount
		{
			[SecurityCritical]
			get
			{
				return this.m_ArgCount;
			}
		}

		// Token: 0x06005AD9 RID: 23257 RVA: 0x0013E040 File Offset: 0x0013C240
		[SecurityCritical]
		public string GetInArgName(int index)
		{
			return null;
		}

		// Token: 0x06005ADA RID: 23258 RVA: 0x0013E043 File Offset: 0x0013C243
		[SecurityCritical]
		public object GetInArg(int argNum)
		{
			return null;
		}

		// Token: 0x17000FA5 RID: 4005
		// (get) Token: 0x06005ADB RID: 23259 RVA: 0x0013E046 File Offset: 0x0013C246
		public object[] InArgs
		{
			[SecurityCritical]
			get
			{
				return null;
			}
		}

		// Token: 0x17000FA6 RID: 4006
		// (get) Token: 0x06005ADC RID: 23260 RVA: 0x0013E049 File Offset: 0x0013C249
		public LogicalCallContext LogicalCallContext
		{
			[SecurityCritical]
			get
			{
				return null;
			}
		}

		// Token: 0x040028D2 RID: 10450
		private string m_URI = "Exception";

		// Token: 0x040028D3 RID: 10451
		private string m_MethodName = "Unknown";

		// Token: 0x040028D4 RID: 10452
		private string m_TypeName = "Unknown";

		// Token: 0x040028D5 RID: 10453
		private object m_MethodSignature;

		// Token: 0x040028D6 RID: 10454
		private int m_ArgCount;

		// Token: 0x040028D7 RID: 10455
		private string m_ArgName = "Unknown";
	}
}
