using System;
using System.Xml;
using Microsoft.Exchange.AirSync.Wbxml;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x020000FA RID: 250
	internal class PictureOptions
	{
		// Token: 0x06000D96 RID: 3478 RVA: 0x0004B144 File Offset: 0x00049344
		internal PictureOptions()
		{
			this.MaxSize = int.MaxValue;
			this.MaxPictures = int.MaxValue;
			this.MaxResolution = GlobalSettings.HDPhotoDefaultSupportedResolution;
			this.userPhotoSize = PictureOptions.GetUserPhotoSize(this.MaxResolution);
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06000D97 RID: 3479 RVA: 0x0004B17E File Offset: 0x0004937E
		// (set) Token: 0x06000D98 RID: 3480 RVA: 0x0004B186 File Offset: 0x00049386
		internal int MaxSize { get; set; }

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06000D99 RID: 3481 RVA: 0x0004B18F File Offset: 0x0004938F
		// (set) Token: 0x06000D9A RID: 3482 RVA: 0x0004B197 File Offset: 0x00049397
		internal int MaxPictures { get; set; }

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06000D9B RID: 3483 RVA: 0x0004B1A0 File Offset: 0x000493A0
		// (set) Token: 0x06000D9C RID: 3484 RVA: 0x0004B1A8 File Offset: 0x000493A8
		internal UserPhotoResolution MaxResolution { get; set; }

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06000D9D RID: 3485 RVA: 0x0004B1B1 File Offset: 0x000493B1
		internal UserPhotoSize PhotoSize
		{
			get
			{
				return this.userPhotoSize;
			}
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x0004B1BC File Offset: 0x000493BC
		internal static PictureOptions Parse(XmlNode pictureOptionsNode, StatusCode protocolError)
		{
			if (pictureOptionsNode == null)
			{
				return null;
			}
			PictureOptions pictureOptions = new PictureOptions();
			foreach (object obj in pictureOptionsNode.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				string localName;
				if ((localName = xmlNode.LocalName) != null)
				{
					if (!(localName == "MaxSize"))
					{
						if (!(localName == "MaxPictures"))
						{
							if (localName == "MaxResolution")
							{
								UserPhotoResolution userPhotoResolution;
								if (!Enum.TryParse<UserPhotoResolution>(xmlNode.InnerText, out userPhotoResolution))
								{
									throw new AirSyncPermanentException(protocolError, false)
									{
										ErrorStringForProtocolLogger = "InvalidMaxResolution"
									};
								}
								pictureOptions.MaxResolution = userPhotoResolution;
								pictureOptions.userPhotoSize = PictureOptions.GetUserPhotoSize(userPhotoResolution);
								continue;
							}
						}
						else
						{
							int num;
							if (!int.TryParse(xmlNode.InnerText, out num))
							{
								throw new AirSyncPermanentException(protocolError, false)
								{
									ErrorStringForProtocolLogger = "InvalidMaxPictures"
								};
							}
							pictureOptions.MaxPictures = num;
							continue;
						}
					}
					else
					{
						int num;
						if (!int.TryParse(xmlNode.InnerText, out num))
						{
							throw new AirSyncPermanentException(protocolError, false)
							{
								ErrorStringForProtocolLogger = "InvalidMaxSize"
							};
						}
						pictureOptions.MaxSize = num;
						continue;
					}
				}
				throw new AirSyncPermanentException(protocolError, false)
				{
					ErrorStringForProtocolLogger = "UnexpectedPictureOption:" + xmlNode.Name
				};
			}
			return pictureOptions;
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x0004B328 File Offset: 0x00049528
		private static UserPhotoSize GetUserPhotoSize(UserPhotoResolution resolution)
		{
			switch (resolution)
			{
			case UserPhotoResolution.HR48x48:
				return UserPhotoSize.HR48x48;
			case UserPhotoResolution.HR64x64:
				return UserPhotoSize.HR64x64;
			case UserPhotoResolution.HR96x96:
				return UserPhotoSize.HR96x96;
			case UserPhotoResolution.HR120x120:
				return UserPhotoSize.HR120x120;
			case UserPhotoResolution.HR240x240:
				return UserPhotoSize.HR240x240;
			case UserPhotoResolution.HR360x360:
				return UserPhotoSize.HR360x360;
			case UserPhotoResolution.HR432x432:
				return UserPhotoSize.HR432x432;
			case UserPhotoResolution.HR504x504:
				return UserPhotoSize.HR504x504;
			case UserPhotoResolution.HR648x648:
				return UserPhotoSize.HR648x648;
			default:
				throw new ArgumentOutOfRangeException("Invalid value for UserPhotoResolution parameter.");
			}
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x0004B380 File Offset: 0x00049580
		internal XmlNode CreatePictureNode(XmlDocument document, string namespaceName, byte[] picture, bool pictureLimitReached, out bool pictureWasAdded)
		{
			pictureWasAdded = false;
			XmlNode xmlNode = document.CreateElement("Picture", namespaceName);
			StatusCode statusCode = StatusCode.Success;
			if (picture == null || picture.Length == 0)
			{
				statusCode = StatusCode.NoPicture;
			}
			else if (picture.Length > this.MaxSize)
			{
				statusCode = StatusCode.PictureTooLarge;
			}
			else if (pictureLimitReached)
			{
				statusCode = StatusCode.PictureLimitReached;
			}
			XmlNode xmlNode2 = document.CreateElement("Status", namespaceName);
			XmlNode xmlNode3 = xmlNode2;
			int num = (int)statusCode;
			xmlNode3.InnerText = num.ToString();
			xmlNode.AppendChild(xmlNode2);
			if (statusCode == StatusCode.Success)
			{
				xmlNode.AppendChild(new AirSyncBlobXmlNode(null, "Data", namespaceName, document)
				{
					ByteArray = picture
				});
				pictureWasAdded = true;
			}
			return xmlNode;
		}

		// Token: 0x040008A1 RID: 2209
		private UserPhotoSize userPhotoSize;
	}
}
