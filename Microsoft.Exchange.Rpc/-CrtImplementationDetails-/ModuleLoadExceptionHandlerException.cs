using System;
using System.Runtime.Serialization;
using System.Security;

namespace <CrtImplementationDetails>
{
	// Token: 0x02000413 RID: 1043
	[Serializable]
	internal class ModuleLoadExceptionHandlerException : ModuleLoadException
	{
		// Token: 0x060011BA RID: 4538 RVA: 0x0005ADBC File Offset: 0x0005A1BC
		protected ModuleLoadExceptionHandlerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.NestedException = (Exception)info.GetValue("NestedException", typeof(Exception));
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x0005B410 File Offset: 0x0005A810
		public ModuleLoadExceptionHandlerException(string message, Exception innerException, Exception nestedException) : base(message, innerException)
		{
			this.NestedException = nestedException;
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x060011BC RID: 4540 RVA: 0x0005ACC0 File Offset: 0x0005A0C0
		// (set) Token: 0x060011BD RID: 4541 RVA: 0x0005ACD4 File Offset: 0x0005A0D4
		public Exception NestedException
		{
			get
			{
				return this.<backing_store>NestedException;
			}
			set
			{
				this.<backing_store>NestedException = value;
			}
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x0005ACE8 File Offset: 0x0005A0E8
		public override string ToString()
		{
			string text;
			if (this.InnerException != null)
			{
				text = this.InnerException.ToString();
			}
			else
			{
				text = string.Empty;
			}
			string text2;
			if (this.NestedException != null)
			{
				text2 = this.NestedException.ToString();
			}
			else
			{
				text2 = string.Empty;
			}
			object[] array = new object[4];
			array[0] = this.GetType();
			string text3;
			if (this.Message != null)
			{
				text3 = this.Message;
			}
			else
			{
				text3 = string.Empty;
			}
			array[1] = text3;
			string text4;
			if (text != null)
			{
				text4 = text;
			}
			else
			{
				text4 = string.Empty;
			}
			array[2] = text4;
			string text5;
			if (text2 != null)
			{
				text5 = text2;
			}
			else
			{
				text5 = string.Empty;
			}
			array[3] = text5;
			return string.Format("\n{0}: {1}\n--- Start of primary exception ---\n{2}\n--- End of primary exception ---\n\n--- Start of nested exception ---\n{3}\n--- End of nested exception ---\n", array);
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x0005AD8C File Offset: 0x0005A18C
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("NestedException", this.NestedException, typeof(Exception));
		}

		// Token: 0x04001048 RID: 4168
		private const string formatString = "\n{0}: {1}\n--- Start of primary exception ---\n{2}\n--- End of primary exception ---\n\n--- Start of nested exception ---\n{3}\n--- End of nested exception ---\n";

		// Token: 0x04001049 RID: 4169
		private Exception <backing_store>NestedException;
	}
}
