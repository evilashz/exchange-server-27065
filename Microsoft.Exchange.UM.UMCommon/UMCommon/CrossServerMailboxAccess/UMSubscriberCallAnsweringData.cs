using System;
using System.IO;

namespace Microsoft.Exchange.UM.UMCommon.CrossServerMailboxAccess
{
	// Token: 0x0200008A RID: 138
	internal class UMSubscriberCallAnsweringData
	{
		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060004C7 RID: 1223 RVA: 0x00011B98 File Offset: 0x0000FD98
		// (set) Token: 0x060004C8 RID: 1224 RVA: 0x00011BA0 File Offset: 0x0000FDA0
		public ITempWavFile Greeting { get; set; }

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x00011BAC File Offset: 0x0000FDAC
		public byte[] RawGreeting
		{
			get
			{
				byte[] array = null;
				if (this.Greeting != null)
				{
					using (FileStream fileStream = new FileStream(this.Greeting.FilePath, FileMode.Open))
					{
						array = new byte[fileStream.Length];
						fileStream.Read(array, 0, array.Length);
					}
				}
				return array;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060004CA RID: 1226 RVA: 0x00011C0C File Offset: 0x0000FE0C
		// (set) Token: 0x060004CB RID: 1227 RVA: 0x00011C14 File Offset: 0x0000FE14
		public bool IsOOF { get; set; }

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060004CC RID: 1228 RVA: 0x00011C1D File Offset: 0x0000FE1D
		// (set) Token: 0x060004CD RID: 1229 RVA: 0x00011C25 File Offset: 0x0000FE25
		public TranscriptionEnabledSetting IsTranscriptionEnabledInMailboxConfig { get; set; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x00011C2E File Offset: 0x0000FE2E
		// (set) Token: 0x060004CF RID: 1231 RVA: 0x00011C36 File Offset: 0x0000FE36
		public bool IsMailboxQuotaExceeded { get; set; }

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x00011C3F File Offset: 0x0000FE3F
		// (set) Token: 0x060004D1 RID: 1233 RVA: 0x00011C47 File Offset: 0x0000FE47
		public bool TaskTimedOut { get; set; }

		// Token: 0x060004D2 RID: 1234 RVA: 0x00011C50 File Offset: 0x0000FE50
		public UMSubscriberCallAnsweringData()
		{
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x00011C58 File Offset: 0x0000FE58
		public UMSubscriberCallAnsweringData(byte[] greetingBytes, string greetingName, bool isOOF, TranscriptionEnabledSetting isTranscriptionEnabledInMailboxConfig, bool isMailboxQuotaExceeded, bool taskTimedOut) : this(UMSubscriberCallAnsweringData.CreateTemporaryGreetingFile(greetingBytes, greetingName), isOOF, isTranscriptionEnabledInMailboxConfig, isMailboxQuotaExceeded, taskTimedOut)
		{
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x00011C6E File Offset: 0x0000FE6E
		public UMSubscriberCallAnsweringData(ITempWavFile greeting, bool isOOF, TranscriptionEnabledSetting isTranscriptionEnabledInMailboxConfig, bool isMailboxQuotaExceeded, bool taskTimedOut)
		{
			this.IsOOF = isOOF;
			this.IsTranscriptionEnabledInMailboxConfig = isTranscriptionEnabledInMailboxConfig;
			this.IsMailboxQuotaExceeded = isMailboxQuotaExceeded;
			this.TaskTimedOut = taskTimedOut;
			this.Greeting = greeting;
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x00011C9C File Offset: 0x0000FE9C
		private static ITempWavFile CreateTemporaryGreetingFile(byte[] greetingBytes, string extraInfo)
		{
			ITempWavFile tempWavFile = null;
			if (greetingBytes != null && greetingBytes.Length > 0)
			{
				tempWavFile = TempFileFactory.CreateTempWavFile();
				using (FileStream fileStream = new FileStream(tempWavFile.FilePath, FileMode.Create))
				{
					fileStream.Write(greetingBytes, 0, greetingBytes.Length);
				}
				tempWavFile.ExtraInfo = extraInfo;
			}
			return tempWavFile;
		}
	}
}
