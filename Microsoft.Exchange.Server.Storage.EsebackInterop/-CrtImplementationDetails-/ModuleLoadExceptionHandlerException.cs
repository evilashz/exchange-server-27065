using System;
using System.Runtime.Serialization;
using System.Security;

namespace <CrtImplementationDetails>
{
	// Token: 0x02000103 RID: 259
	[Serializable]
	internal class ModuleLoadExceptionHandlerException : ModuleLoadException
	{
		// Token: 0x060000BA RID: 186 RVA: 0x00002938 File Offset: 0x00001D38
		protected ModuleLoadExceptionHandlerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.NestedException = (Exception)info.GetValue("NestedException", typeof(Exception));
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00002F8C File Offset: 0x0000238C
		public ModuleLoadExceptionHandlerException(string message, Exception innerException, Exception nestedException) : base(message, innerException)
		{
			this.NestedException = nestedException;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060000BC RID: 188 RVA: 0x0000283C File Offset: 0x00001C3C
		// (set) Token: 0x060000BD RID: 189 RVA: 0x00002850 File Offset: 0x00001C50
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

		// Token: 0x060000BE RID: 190 RVA: 0x00002864 File Offset: 0x00001C64
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

		// Token: 0x060000BF RID: 191 RVA: 0x00002908 File Offset: 0x00001D08
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("NestedException", this.NestedException, typeof(Exception));
		}

		// Token: 0x04000197 RID: 407
		private const string formatString = "\n{0}: {1}\n--- Start of primary exception ---\n{2}\n--- End of primary exception ---\n\n--- Start of nested exception ---\n{3}\n--- End of nested exception ---\n";

		// Token: 0x04000198 RID: 408
		private Exception <backing_store>NestedException;
	}
}
