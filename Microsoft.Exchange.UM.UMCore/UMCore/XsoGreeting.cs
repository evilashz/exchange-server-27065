using System;
using System.IO;
using Microsoft.Exchange.Audio;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000240 RID: 576
	internal class XsoGreeting : GreetingBase
	{
		// Token: 0x060010F3 RID: 4339 RVA: 0x0004BACC File Offset: 0x00049CCC
		internal XsoGreeting(UMSubscriber umuser, string name)
		{
			this.umuser = umuser;
			this.name = name;
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x060010F4 RID: 4340 RVA: 0x0004BAE2 File Offset: 0x00049CE2
		internal override string Name
		{
			get
			{
				return "Um.CustomGreetings." + this.name;
			}
		}

		// Token: 0x060010F5 RID: 4341 RVA: 0x0004BAF4 File Offset: 0x00049CF4
		internal override void Put(string sourceFileName)
		{
			ITempWavFile tempWavFile = null;
			try
			{
				using (PcmReader pcmReader = new PcmReader(sourceFileName))
				{
					if (pcmReader.WaveFormat.SamplesPerSec == 16000)
					{
						tempWavFile = MediaMethods.Pcm16ToPcm8(pcmReader, false);
						sourceFileName = tempWavFile.FilePath;
					}
				}
				using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = this.umuser.CreateSessionLock())
				{
					using (UserConfiguration configuration = this.GetConfiguration(mailboxSessionLock.Session, true))
					{
						using (Stream stream = configuration.GetStream())
						{
							using (FileStream fileStream = new FileStream(sourceFileName, FileMode.Open, FileAccess.Read))
							{
								stream.SetLength(0L);
								CommonUtil.CopyStream(fileStream, stream);
								configuration.Save();
							}
						}
					}
				}
			}
			finally
			{
				if (tempWavFile != null)
				{
					tempWavFile.Dispose();
				}
			}
		}

		// Token: 0x060010F6 RID: 4342 RVA: 0x0004BC08 File Offset: 0x00049E08
		internal override void Delete()
		{
			using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = this.umuser.CreateSessionLock())
			{
				mailboxSessionLock.Session.UserConfigurationManager.DeleteMailboxConfigurations(new string[]
				{
					this.Name
				});
			}
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x0004BC60 File Offset: 0x00049E60
		internal override ITempWavFile Get()
		{
			ITempWavFile result;
			using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = this.umuser.CreateSessionLock())
			{
				using (UserConfiguration configuration = this.GetConfiguration(mailboxSessionLock.Session, false))
				{
					if (configuration == null)
					{
						result = null;
					}
					else
					{
						FileStream fileStream = null;
						Stream stream = null;
						try
						{
							ITempWavFile tempWavFile = TempFileFactory.CreateTempWavFile();
							fileStream = new FileStream(tempWavFile.FilePath, FileMode.Create);
							stream = configuration.GetStream();
							CommonUtil.CopyStream(stream, fileStream);
							result = tempWavFile;
						}
						catch (CorruptDataException)
						{
							this.Delete();
							result = null;
						}
						catch (InvalidOperationException)
						{
							this.Delete();
							result = null;
						}
						finally
						{
							if (fileStream != null)
							{
								fileStream.Dispose();
							}
							if (stream != null)
							{
								stream.Dispose();
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060010F8 RID: 4344 RVA: 0x0004BD40 File Offset: 0x00049F40
		internal override bool Exists()
		{
			bool result;
			using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = this.umuser.CreateSessionLock())
			{
				using (UserConfiguration configuration = this.GetConfiguration(mailboxSessionLock.Session, false))
				{
					result = (null != configuration);
				}
			}
			return result;
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x0004BDA4 File Offset: 0x00049FA4
		protected override void InternalDispose(bool disposing)
		{
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x0004BDA6 File Offset: 0x00049FA6
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<XsoGreeting>(this);
		}

		// Token: 0x060010FB RID: 4347 RVA: 0x0004BDB0 File Offset: 0x00049FB0
		private UserConfiguration GetConfiguration(MailboxSession session, bool create)
		{
			UserConfiguration result = null;
			try
			{
				result = session.UserConfigurationManager.GetMailboxConfiguration(this.Name, UserConfigurationTypes.Stream);
			}
			catch (ObjectNotFoundException)
			{
				if (create)
				{
					result = session.UserConfigurationManager.CreateMailboxConfiguration(this.Name, UserConfigurationTypes.Stream);
				}
			}
			catch (CorruptDataException)
			{
				this.Delete();
				if (create)
				{
					result = session.UserConfigurationManager.CreateMailboxConfiguration(this.Name, UserConfigurationTypes.Stream);
				}
			}
			catch (InvalidOperationException)
			{
				this.Delete();
				if (create)
				{
					result = session.UserConfigurationManager.CreateMailboxConfiguration(this.Name, UserConfigurationTypes.Stream);
				}
			}
			return result;
		}

		// Token: 0x04000BAB RID: 2987
		private UMSubscriber umuser;

		// Token: 0x04000BAC RID: 2988
		private string name;
	}
}
