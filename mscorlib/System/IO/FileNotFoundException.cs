using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;

namespace System.IO
{
	// Token: 0x02000187 RID: 391
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class FileNotFoundException : IOException
	{
		// Token: 0x06001821 RID: 6177 RVA: 0x0004D6AC File Offset: 0x0004B8AC
		[__DynamicallyInvokable]
		public FileNotFoundException() : base(Environment.GetResourceString("IO.FileNotFound"))
		{
			base.SetErrorCode(-2147024894);
		}

		// Token: 0x06001822 RID: 6178 RVA: 0x0004D6C9 File Offset: 0x0004B8C9
		[__DynamicallyInvokable]
		public FileNotFoundException(string message) : base(message)
		{
			base.SetErrorCode(-2147024894);
		}

		// Token: 0x06001823 RID: 6179 RVA: 0x0004D6DD File Offset: 0x0004B8DD
		[__DynamicallyInvokable]
		public FileNotFoundException(string message, Exception innerException) : base(message, innerException)
		{
			base.SetErrorCode(-2147024894);
		}

		// Token: 0x06001824 RID: 6180 RVA: 0x0004D6F2 File Offset: 0x0004B8F2
		[__DynamicallyInvokable]
		public FileNotFoundException(string message, string fileName) : base(message)
		{
			base.SetErrorCode(-2147024894);
			this._fileName = fileName;
		}

		// Token: 0x06001825 RID: 6181 RVA: 0x0004D70D File Offset: 0x0004B90D
		[__DynamicallyInvokable]
		public FileNotFoundException(string message, string fileName, Exception innerException) : base(message, innerException)
		{
			base.SetErrorCode(-2147024894);
			this._fileName = fileName;
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06001826 RID: 6182 RVA: 0x0004D729 File Offset: 0x0004B929
		[__DynamicallyInvokable]
		public override string Message
		{
			[__DynamicallyInvokable]
			get
			{
				this.SetMessageField();
				return this._message;
			}
		}

		// Token: 0x06001827 RID: 6183 RVA: 0x0004D738 File Offset: 0x0004B938
		private void SetMessageField()
		{
			if (this._message == null)
			{
				if (this._fileName == null && base.HResult == -2146233088)
				{
					this._message = Environment.GetResourceString("IO.FileNotFound");
					return;
				}
				if (this._fileName != null)
				{
					this._message = FileLoadException.FormatFileLoadExceptionMessage(this._fileName, base.HResult);
				}
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06001828 RID: 6184 RVA: 0x0004D792 File Offset: 0x0004B992
		[__DynamicallyInvokable]
		public string FileName
		{
			[__DynamicallyInvokable]
			get
			{
				return this._fileName;
			}
		}

		// Token: 0x06001829 RID: 6185 RVA: 0x0004D79C File Offset: 0x0004B99C
		[__DynamicallyInvokable]
		public override string ToString()
		{
			string text = base.GetType().FullName + ": " + this.Message;
			if (this._fileName != null && this._fileName.Length != 0)
			{
				text = text + Environment.NewLine + Environment.GetResourceString("IO.FileName_Name", new object[]
				{
					this._fileName
				});
			}
			if (base.InnerException != null)
			{
				text = text + " ---> " + base.InnerException.ToString();
			}
			if (this.StackTrace != null)
			{
				text = text + Environment.NewLine + this.StackTrace;
			}
			try
			{
				if (this.FusionLog != null)
				{
					if (text == null)
					{
						text = " ";
					}
					text += Environment.NewLine;
					text += Environment.NewLine;
					text += this.FusionLog;
				}
			}
			catch (SecurityException)
			{
			}
			return text;
		}

		// Token: 0x0600182A RID: 6186 RVA: 0x0004D888 File Offset: 0x0004BA88
		protected FileNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this._fileName = info.GetString("FileNotFound_FileName");
			try
			{
				this._fusionLog = info.GetString("FileNotFound_FusionLog");
			}
			catch
			{
				this._fusionLog = null;
			}
		}

		// Token: 0x0600182B RID: 6187 RVA: 0x0004D8DC File Offset: 0x0004BADC
		private FileNotFoundException(string fileName, string fusionLog, int hResult) : base(null)
		{
			base.SetErrorCode(hResult);
			this._fileName = fileName;
			this._fusionLog = fusionLog;
			this.SetMessageField();
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x0600182C RID: 6188 RVA: 0x0004D900 File Offset: 0x0004BB00
		public string FusionLog
		{
			[SecuritySafeCritical]
			[SecurityPermission(SecurityAction.Demand, Flags = (SecurityPermissionFlag.ControlEvidence | SecurityPermissionFlag.ControlPolicy))]
			get
			{
				return this._fusionLog;
			}
		}

		// Token: 0x0600182D RID: 6189 RVA: 0x0004D908 File Offset: 0x0004BB08
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("FileNotFound_FileName", this._fileName, typeof(string));
			try
			{
				info.AddValue("FileNotFound_FusionLog", this.FusionLog, typeof(string));
			}
			catch (SecurityException)
			{
			}
		}

		// Token: 0x0400083E RID: 2110
		private string _fileName;

		// Token: 0x0400083F RID: 2111
		private string _fusionLog;
	}
}
