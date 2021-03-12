using System;
using System.Diagnostics;
using System.Security.AccessControl;
using Microsoft.Exchange.Diagnostics.Components.Common;

namespace Microsoft.Exchange.Security.Authorization
{
	// Token: 0x0200002D RID: 45
	[DebuggerDisplay("RawSecurityDescriptor = {RawSecurityDescriptor}")]
	public class SecurityDescriptor
	{
		// Token: 0x06000121 RID: 289 RVA: 0x00006256 File Offset: 0x00004456
		public SecurityDescriptor(byte[] binaryForm)
		{
			this.binaryForm = binaryForm;
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00006265 File Offset: 0x00004465
		public byte[] BinaryForm
		{
			get
			{
				return this.binaryForm;
			}
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00006270 File Offset: 0x00004470
		public static SecurityDescriptor FromRawSecurityDescriptor(RawSecurityDescriptor rawSecurityDescriptor)
		{
			if (rawSecurityDescriptor != null)
			{
				byte[] array = new byte[rawSecurityDescriptor.BinaryLength];
				rawSecurityDescriptor.GetBinaryForm(array, 0);
				return new SecurityDescriptor(array);
			}
			return null;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x0000629C File Offset: 0x0000449C
		public RawSecurityDescriptor ToRawSecurityDescriptorThrow()
		{
			if (this.binaryForm == null)
			{
				return null;
			}
			return new RawSecurityDescriptor(this.binaryForm, 0);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x000062B4 File Offset: 0x000044B4
		public RawSecurityDescriptor ToRawSecurityDescriptor()
		{
			RawSecurityDescriptor result = null;
			if (this.binaryForm == null)
			{
				return null;
			}
			Exception ex = null;
			try
			{
				result = new RawSecurityDescriptor(this.binaryForm, 0);
			}
			catch (ArgumentOutOfRangeException ex2)
			{
				ex = ex2;
			}
			catch (ArgumentNullException ex3)
			{
				ex = ex3;
			}
			catch (ArgumentException ex4)
			{
				ex = ex4;
			}
			catch (FormatException ex5)
			{
				ex = ex5;
			}
			catch (OverflowException ex6)
			{
				ex = ex6;
			}
			catch (InvalidOperationException ex7)
			{
				ex = ex7;
			}
			if (ex != null)
			{
				ExTraceGlobals.AuthorizationTracer.TraceWarning<Type, string>(0L, "SecurityDescriptor::RawSecurityDescriptor - failed to create RawSecurityDescriptor with {0}, {1}", ex.GetType(), ex.Message);
				return null;
			}
			return result;
		}

		// Token: 0x040000B6 RID: 182
		private byte[] binaryForm;
	}
}
