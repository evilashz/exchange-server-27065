using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Management.Automation;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Audio;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x020000AF RID: 175
	[Cmdlet("Import", "RecipientDataProperty", DefaultParameterSetName = "ImportPicture", SupportsShouldProcess = true)]
	public sealed class ImportRecipientDataProperty : RecipientObjectActionTask<MailboxUserContactIdParameter, ADRecipient>
	{
		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06000B02 RID: 2818 RVA: 0x0002F517 File Offset: 0x0002D717
		// (set) Token: 0x06000B03 RID: 2819 RVA: 0x0002F52E File Offset: 0x0002D72E
		[Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 0)]
		public override MailboxUserContactIdParameter Identity
		{
			get
			{
				return (MailboxUserContactIdParameter)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06000B04 RID: 2820 RVA: 0x0002F541 File Offset: 0x0002D741
		// (set) Token: 0x06000B05 RID: 2821 RVA: 0x0002F558 File Offset: 0x0002D758
		[Parameter(Mandatory = true)]
		public byte[] FileData
		{
			get
			{
				return (byte[])base.Fields["FileData"];
			}
			set
			{
				base.Fields["FileData"] = value;
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06000B06 RID: 2822 RVA: 0x0002F56B File Offset: 0x0002D76B
		// (set) Token: 0x06000B07 RID: 2823 RVA: 0x0002F591 File Offset: 0x0002D791
		[Parameter(ParameterSetName = "ImportPicture")]
		public SwitchParameter Picture
		{
			get
			{
				return (SwitchParameter)(base.Fields["Picture"] ?? false);
			}
			set
			{
				base.Fields["Picture"] = value;
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06000B08 RID: 2824 RVA: 0x0002F5A9 File Offset: 0x0002D7A9
		// (set) Token: 0x06000B09 RID: 2825 RVA: 0x0002F5CF File Offset: 0x0002D7CF
		[Parameter(ParameterSetName = "ImportSpokenName")]
		public SwitchParameter SpokenName
		{
			get
			{
				return (SwitchParameter)(base.Fields["SpokenName"] ?? false);
			}
			set
			{
				base.Fields["SpokenName"] = value;
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06000B0A RID: 2826 RVA: 0x0002F5E7 File Offset: 0x0002D7E7
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageImportRecipientDataProperty(this.Identity.ToString());
			}
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x0002F5FC File Offset: 0x0002D7FC
		protected override void InternalValidate()
		{
			if (this.Picture.IsPresent)
			{
				this.ValidateJPEGFile(this.FileData);
			}
			else if (this.SpokenName.IsPresent)
			{
				this.ValidateWMAFile(this.FileData);
			}
			else
			{
				base.WriteError(new LocalizedException(Strings.ErrorUseDataPropertyNameParameter), ErrorCategory.InvalidData, null);
			}
			base.InternalValidate();
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x0002F660 File Offset: 0x0002D860
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			ADRecipient adrecipient = (ADRecipient)base.PrepareDataObject();
			if (this.Picture.IsPresent)
			{
				adrecipient.ThumbnailPhoto = this.data;
			}
			else if (this.SpokenName.IsPresent)
			{
				adrecipient.UMSpokenName = this.data;
			}
			TaskLogger.LogExit();
			return adrecipient;
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x0002F6C0 File Offset: 0x0002D8C0
		private void ValidateJPEGFile(byte[] jpegFileData)
		{
			try
			{
				using (Stream stream = new MemoryStream(jpegFileData))
				{
					if (stream.Length > (long)ImportRecipientDataProperty.MaxJpegSize.ToBytes())
					{
						base.WriteError(new ArgumentException(Strings.ErrorJPEGFileTooBig), ErrorCategory.InvalidData, null);
					}
					Image image = Image.FromStream(stream);
					if (image.RawFormat.Guid != ImageFormat.Jpeg.Guid)
					{
						throw new ArgumentException();
					}
					this.data = jpegFileData;
				}
			}
			catch (ArgumentException)
			{
				base.WriteError(new FormatException(Strings.ErrorInvalidJPEGFormat), ErrorCategory.InvalidData, null);
			}
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x0002F774 File Offset: 0x0002D974
		private void ValidateWMAFile(byte[] wmaFileData)
		{
			bool flag = true;
			Exception innerException = null;
			string text = null;
			string text2 = null;
			string text3 = null;
			try
			{
				text = Path.GetTempFileName();
				text2 = Path.GetTempFileName();
				text3 = Path.GetTempFileName();
				using (FileStream fileStream = new FileStream(text3, FileMode.Create, FileAccess.ReadWrite, FileShare.None))
				{
					BinaryWriter binaryWriter = new BinaryWriter(fileStream);
					binaryWriter.Write(wmaFileData);
					fileStream.Flush();
				}
				using (WmaReader wmaReader = new WmaReader(text3))
				{
					using (PcmWriter pcmWriter = new PcmWriter(text, wmaReader.Format))
					{
						byte[] array = new byte[wmaReader.SampleSize * 2];
						int count;
						while ((count = wmaReader.Read(array, array.Length)) > 0)
						{
							pcmWriter.Write(array, count);
						}
					}
				}
				using (PcmReader pcmReader = new PcmReader(text))
				{
					using (WmaWriter wmaWriter = new Wma8Writer(text2, pcmReader.WaveFormat))
					{
						byte[] array2 = new byte[wmaWriter.BufferSize];
						double num = 0.0;
						int num2;
						while ((num2 = pcmReader.Read(array2, array2.Length)) > 0)
						{
							AudioNormalizer.ProcessBuffer(array2, num2, 9E-05, 0.088, ref num);
							wmaWriter.Write(array2, num2);
						}
					}
				}
				using (FileStream fileStream2 = new FileStream(text2, FileMode.Open, FileAccess.Read, FileShare.Read))
				{
					if (fileStream2.Length > (long)ImportRecipientDataProperty.MaxWmaSize.ToBytes())
					{
						base.WriteError(new ArgumentException(Strings.ErrorWMAFileTooBig), ErrorCategory.InvalidData, null);
					}
					BinaryReader binaryReader = new BinaryReader(fileStream2);
					this.data = binaryReader.ReadBytes((int)binaryReader.BaseStream.Length);
				}
			}
			catch (InvalidWmaFormatException ex)
			{
				flag = false;
				innerException = ex;
			}
			catch (UnsupportedAudioFormat unsupportedAudioFormat)
			{
				flag = false;
				innerException = unsupportedAudioFormat;
			}
			catch (COMException ex2)
			{
				flag = false;
				innerException = ex2;
			}
			catch (IOException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidOperation, null);
			}
			finally
			{
				if (File.Exists(text))
				{
					File.Delete(text);
				}
				if (File.Exists(text2))
				{
					File.Delete(text2);
				}
				if (File.Exists(text3))
				{
					File.Delete(text3);
				}
				if (!flag)
				{
					base.WriteError(new FormatException(Strings.ErrorInvalidWMAFormat, innerException), ErrorCategory.InvalidData, null);
				}
			}
		}

		// Token: 0x0400026F RID: 623
		private byte[] data;

		// Token: 0x04000270 RID: 624
		internal static readonly ByteQuantifiedSize MaxJpegSize = ByteQuantifiedSize.FromKB(10UL);

		// Token: 0x04000271 RID: 625
		internal static readonly ByteQuantifiedSize MaxWmaSize = ByteQuantifiedSize.FromKB(32UL);
	}
}
