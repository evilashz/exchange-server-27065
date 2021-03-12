using System;
using System.IO;
using Microsoft.Exchange.Audio;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000013 RID: 19
	internal class ADGreeting : GreetingBase
	{
		// Token: 0x06000118 RID: 280 RVA: 0x000057BE File Offset: 0x000039BE
		internal ADGreeting(ADRecipient aduser, string name)
		{
			this.aduser = aduser;
			this.name = name;
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000119 RID: 281 RVA: 0x000057D4 File Offset: 0x000039D4
		internal override string Name
		{
			get
			{
				return "Um.CustomGreetings." + this.name;
			}
		}

		// Token: 0x0600011A RID: 282 RVA: 0x000057E8 File Offset: 0x000039E8
		internal override void Put(string sourceFileName)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.XsoTracer, this, "ADGreeting::Put().", new object[0]);
			using (ITempFile tempFile = TempFileFactory.CreateTempWmaFile())
			{
				using (PcmReader pcmReader = new PcmReader(sourceFileName))
				{
					using (WmaWriter wmaWriter = new Wma8Writer(tempFile.FilePath, pcmReader.WaveFormat))
					{
						MediaMethods.ConvertWavToWma(pcmReader, wmaWriter);
					}
				}
				FileInfo fileInfo = new FileInfo(tempFile.FilePath);
				int num = Convert.ToInt32(fileInfo.Length);
				if (num > 32768)
				{
					throw new MaxGreetingLengthExceededException(num);
				}
				byte[] array = new byte[num];
				using (FileStream fileStream = new FileStream(tempFile.FilePath, FileMode.Open, FileAccess.Read))
				{
					int num2 = 0;
					int num3;
					do
					{
						num3 = fileStream.Read(array, num2, array.Length - num2);
						num2 += num3;
					}
					while (0 < num3);
				}
				this.aduser.UMSpokenName = array;
				this.aduser.Session.Save(this.aduser);
			}
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00005924 File Offset: 0x00003B24
		internal override void Delete()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.XsoTracer, this, "ADGreeting::Delete().", new object[0]);
			this.aduser.UMSpokenName = null;
			this.aduser.Session.Save(this.aduser);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00005960 File Offset: 0x00003B60
		internal override ITempWavFile Get()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.XsoTracer, this, "ADGreeting::Get().", new object[0]);
			if (!this.Exists())
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.XsoTracer, this, "ADGreeting::Get No Spoken Name.", new object[0]);
				return null;
			}
			byte[] umspokenName = this.aduser.UMSpokenName;
			ITempFile tempFile = TempFileFactory.CreateTempWmaFile();
			ITempWavFile tempWavFile = TempFileFactory.CreateTempWavFile();
			MemoryStream memoryStream = null;
			FileStream fileStream = null;
			ITempWavFile result;
			try
			{
				memoryStream = new MemoryStream(umspokenName);
				fileStream = new FileStream(tempFile.FilePath, FileMode.OpenOrCreate, FileAccess.Write);
				CommonUtil.CopyStream(memoryStream, fileStream);
				fileStream.Close();
				MediaMethods.ConvertWmaToWav(tempFile.FilePath, tempWavFile.FilePath);
				result = tempWavFile;
			}
			catch (WmaToWavConversionException ex)
			{
				CallIdTracer.TraceError(ExTraceGlobals.XsoTracer, this, "ADGreeting::Get WmaToWav conversion failed with e={0}.", new object[]
				{
					ex
				});
				result = null;
			}
			finally
			{
				if (memoryStream != null)
				{
					memoryStream.Close();
				}
				if (fileStream != null)
				{
					fileStream.Close();
				}
			}
			return result;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00005A58 File Offset: 0x00003C58
		internal override bool Exists()
		{
			return this.aduser.UMSpokenName != null && 0 < this.aduser.UMSpokenName.Length;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00005A79 File Offset: 0x00003C79
		protected override void InternalDispose(bool disposing)
		{
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00005A7B File Offset: 0x00003C7B
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ADGreeting>(this);
		}

		// Token: 0x04000057 RID: 87
		private ADRecipient aduser;

		// Token: 0x04000058 RID: 88
		private string name;
	}
}
