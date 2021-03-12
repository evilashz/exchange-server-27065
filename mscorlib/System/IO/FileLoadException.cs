using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;

namespace System.IO
{
	// Token: 0x02000185 RID: 389
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class FileLoadException : IOException
	{
		// Token: 0x06001811 RID: 6161 RVA: 0x0004D3F4 File Offset: 0x0004B5F4
		[__DynamicallyInvokable]
		public FileLoadException() : base(Environment.GetResourceString("IO.FileLoad"))
		{
			base.SetErrorCode(-2146232799);
		}

		// Token: 0x06001812 RID: 6162 RVA: 0x0004D411 File Offset: 0x0004B611
		[__DynamicallyInvokable]
		public FileLoadException(string message) : base(message)
		{
			base.SetErrorCode(-2146232799);
		}

		// Token: 0x06001813 RID: 6163 RVA: 0x0004D425 File Offset: 0x0004B625
		[__DynamicallyInvokable]
		public FileLoadException(string message, Exception inner) : base(message, inner)
		{
			base.SetErrorCode(-2146232799);
		}

		// Token: 0x06001814 RID: 6164 RVA: 0x0004D43A File Offset: 0x0004B63A
		[__DynamicallyInvokable]
		public FileLoadException(string message, string fileName) : base(message)
		{
			base.SetErrorCode(-2146232799);
			this._fileName = fileName;
		}

		// Token: 0x06001815 RID: 6165 RVA: 0x0004D455 File Offset: 0x0004B655
		[__DynamicallyInvokable]
		public FileLoadException(string message, string fileName, Exception inner) : base(message, inner)
		{
			base.SetErrorCode(-2146232799);
			this._fileName = fileName;
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06001816 RID: 6166 RVA: 0x0004D471 File Offset: 0x0004B671
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

		// Token: 0x06001817 RID: 6167 RVA: 0x0004D47F File Offset: 0x0004B67F
		private void SetMessageField()
		{
			if (this._message == null)
			{
				this._message = FileLoadException.FormatFileLoadExceptionMessage(this._fileName, base.HResult);
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06001818 RID: 6168 RVA: 0x0004D4A0 File Offset: 0x0004B6A0
		[__DynamicallyInvokable]
		public string FileName
		{
			[__DynamicallyInvokable]
			get
			{
				return this._fileName;
			}
		}

		// Token: 0x06001819 RID: 6169 RVA: 0x0004D4A8 File Offset: 0x0004B6A8
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

		// Token: 0x0600181A RID: 6170 RVA: 0x0004D594 File Offset: 0x0004B794
		protected FileLoadException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this._fileName = info.GetString("FileLoad_FileName");
			try
			{
				this._fusionLog = info.GetString("FileLoad_FusionLog");
			}
			catch
			{
				this._fusionLog = null;
			}
		}

		// Token: 0x0600181B RID: 6171 RVA: 0x0004D5E8 File Offset: 0x0004B7E8
		private FileLoadException(string fileName, string fusionLog, int hResult) : base(null)
		{
			base.SetErrorCode(hResult);
			this._fileName = fileName;
			this._fusionLog = fusionLog;
			this.SetMessageField();
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x0600181C RID: 6172 RVA: 0x0004D60C File Offset: 0x0004B80C
		public string FusionLog
		{
			[SecuritySafeCritical]
			[SecurityPermission(SecurityAction.Demand, Flags = (SecurityPermissionFlag.ControlEvidence | SecurityPermissionFlag.ControlPolicy))]
			get
			{
				return this._fusionLog;
			}
		}

		// Token: 0x0600181D RID: 6173 RVA: 0x0004D614 File Offset: 0x0004B814
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("FileLoad_FileName", this._fileName, typeof(string));
			try
			{
				info.AddValue("FileLoad_FusionLog", this.FusionLog, typeof(string));
			}
			catch (SecurityException)
			{
			}
		}

		// Token: 0x0600181E RID: 6174 RVA: 0x0004D674 File Offset: 0x0004B874
		[SecuritySafeCritical]
		internal static string FormatFileLoadExceptionMessage(string fileName, int hResult)
		{
			string format = null;
			FileLoadException.GetFileLoadExceptionMessage(hResult, JitHelpers.GetStringHandleOnStack(ref format));
			string arg = null;
			FileLoadException.GetMessageForHR(hResult, JitHelpers.GetStringHandleOnStack(ref arg));
			return string.Format(CultureInfo.CurrentCulture, format, fileName, arg);
		}

		// Token: 0x0600181F RID: 6175
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetFileLoadExceptionMessage(int hResult, StringHandleOnStack retString);

		// Token: 0x06001820 RID: 6176
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetMessageForHR(int hresult, StringHandleOnStack retString);

		// Token: 0x04000835 RID: 2101
		private string _fileName;

		// Token: 0x04000836 RID: 2102
		private string _fusionLog;
	}
}
