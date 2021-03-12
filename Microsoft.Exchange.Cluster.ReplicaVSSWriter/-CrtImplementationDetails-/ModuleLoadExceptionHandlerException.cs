using System;
using System.Runtime.Serialization;
using System.Security;

namespace <CrtImplementationDetails>
{
	// Token: 0x0200022C RID: 556
	[Serializable]
	internal class ModuleLoadExceptionHandlerException : ModuleLoadException
	{
		// Token: 0x06000120 RID: 288 RVA: 0x0000DD48 File Offset: 0x0000D148
		protected ModuleLoadExceptionHandlerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.NestedException = (Exception)info.GetValue("NestedException", typeof(Exception));
		}

		// Token: 0x06000121 RID: 289 RVA: 0x0000E39C File Offset: 0x0000D79C
		public ModuleLoadExceptionHandlerException(string message, Exception innerException, Exception nestedException) : base(message, innerException)
		{
			this.NestedException = nestedException;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000122 RID: 290 RVA: 0x0000DC4C File Offset: 0x0000D04C
		// (set) Token: 0x06000123 RID: 291 RVA: 0x0000DC60 File Offset: 0x0000D060
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

		// Token: 0x06000124 RID: 292 RVA: 0x0000DC74 File Offset: 0x0000D074
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

		// Token: 0x06000125 RID: 293 RVA: 0x0000DD18 File Offset: 0x0000D118
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("NestedException", this.NestedException, typeof(Exception));
		}

		// Token: 0x04000369 RID: 873
		private const string formatString = "\n{0}: {1}\n--- Start of primary exception ---\n{2}\n--- End of primary exception ---\n\n--- Start of nested exception ---\n{3}\n--- End of nested exception ---\n";

		// Token: 0x0400036A RID: 874
		private Exception <backing_store>NestedException;
	}
}
