using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E9D RID: 3741
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class UserPhotoUtilities
	{
		// Token: 0x060081FB RID: 33275 RVA: 0x00238464 File Offset: 0x00236664
		public static Dictionary<UserPhotoSize, Image> GetAllScaleCroppedImages(Stream originalImageStream)
		{
			Util.ThrowOnNullArgument(originalImageStream, "originalImageStream");
			Dictionary<UserPhotoSize, Image> result;
			using (Image imageFromStream = UserPhotoUtilities.GetImageFromStream(originalImageStream, true))
			{
				Dictionary<UserPhotoSize, Image> dictionary = new Dictionary<UserPhotoSize, Image>();
				foreach (UserPhotoSize userPhotoSize in (UserPhotoSize[])Enum.GetValues(typeof(UserPhotoSize)))
				{
					dictionary.Add(userPhotoSize, UserPhotoUtilities.GetImageOfSize(imageFromStream, userPhotoSize));
				}
				result = dictionary;
			}
			return result;
		}

		// Token: 0x060081FC RID: 33276 RVA: 0x002384E8 File Offset: 0x002366E8
		private static Image GetImageOfSize(Image originalImage, UserPhotoSize size)
		{
			Util.ThrowOnNullArgument(originalImage, "originalImage");
			EnumValidator.ThrowIfInvalid<UserPhotoSize>(size, "size");
			return UserPhotoUtilities.GetScaledAndCroppedImage(originalImage, UserPhotoDimensions.GetImageSize(size));
		}

		// Token: 0x060081FD RID: 33277 RVA: 0x0023850C File Offset: 0x0023670C
		private static Image GetImageFromStream(Stream imageStream, bool checkSizes)
		{
			Util.ThrowOnNullArgument(imageStream, "imageStream");
			if (checkSizes)
			{
				if (imageStream.Length <= 0L)
				{
					UserPhotoUtilities.Tracer.TraceError<long, int>(0L, "Stream of length {0} does not meet minimum file size requirement of {1}.", imageStream.Length, 0);
					throw new UserPhotoFileTooSmallException();
				}
				if (imageStream.Length > 20971520L)
				{
					UserPhotoUtilities.Tracer.TraceError<long, int>(0L, "Stream of length {0} exceeds maximum file size requirement of {1}.", imageStream.Length, 20);
					throw new UserPhotoFileTooLargeException();
				}
			}
			Image result;
			try
			{
				Image image = Image.FromStream(imageStream);
				if (checkSizes && Math.Min(image.Height, image.Width) < UserPhotoUtilities.MinImageSize)
				{
					UserPhotoUtilities.Tracer.TraceError<int, int, int>(0L, "Image of {0} x {1} does not meet the minimum size requirement of {2}.", image.Width, image.Height, UserPhotoUtilities.MinImageSize);
					throw new UserPhotoImageTooSmallException();
				}
				result = image;
			}
			catch (ArgumentException)
			{
				UserPhotoUtilities.Tracer.TraceError(0L, "Stream provided did not represent an Image.");
				throw new UserPhotoNotAnImageException();
			}
			return result;
		}

		// Token: 0x060081FE RID: 33278 RVA: 0x002385F4 File Offset: 0x002367F4
		private static Image GetScaledAndCroppedImage(Image image, Size maxImageSize)
		{
			if (maxImageSize.Width > image.Size.Width || maxImageSize.Height > image.Size.Height)
			{
				return null;
			}
			Size imageScaledSize = UserPhotoUtilities.GetImageScaledSize(image.Size, maxImageSize);
			Image result;
			using (Bitmap bitmap = new Bitmap(imageScaledSize.Width, imageScaledSize.Height))
			{
				using (Graphics graphics = Graphics.FromImage(bitmap))
				{
					graphics.CompositingQuality = CompositingQuality.HighQuality;
					graphics.SmoothingMode = SmoothingMode.HighQuality;
					graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
					graphics.DrawImage(image, 0, 0, bitmap.Width, bitmap.Height);
				}
				RectangleF imageCenterCrop = UserPhotoUtilities.GetImageCenterCrop(imageScaledSize, maxImageSize);
				result = bitmap.Clone(imageCenterCrop, bitmap.PixelFormat);
			}
			return result;
		}

		// Token: 0x060081FF RID: 33279 RVA: 0x002386D4 File Offset: 0x002368D4
		private static RectangleF GetImageCenterCrop(Size imageSize, Size maxImageSize)
		{
			PointF imageCenterPosition = UserPhotoUtilities.GetImageCenterPosition(imageSize, maxImageSize);
			float width = (float)Math.Min(imageSize.Width, maxImageSize.Width);
			float height = (float)Math.Min(imageSize.Height, maxImageSize.Height);
			return new RectangleF(imageCenterPosition, new SizeF(width, height));
		}

		// Token: 0x06008200 RID: 33280 RVA: 0x00238720 File Offset: 0x00236920
		private static PointF GetImageCenterPosition(Size imageSize, Size maxImageSize)
		{
			PointF result = new PointF(0f, 0f);
			if (UserPhotoUtilities.NeedsHorizontalCrop(imageSize, maxImageSize))
			{
				result.X = (float)(imageSize.Width - maxImageSize.Width) * 0.5f;
			}
			if (UserPhotoUtilities.NeedsVerticalCrop(imageSize, maxImageSize))
			{
				result.Y = (float)(imageSize.Height - maxImageSize.Height) * 0.35f;
			}
			return result;
		}

		// Token: 0x06008201 RID: 33281 RVA: 0x00238790 File Offset: 0x00236990
		private static float GetVerticalScaleFactor(Size imageSize, Size maxImageSize)
		{
			float result = 1f;
			if (UserPhotoUtilities.NeedsVerticalCrop(imageSize, maxImageSize))
			{
				result = (float)maxImageSize.Height / (float)imageSize.Height;
			}
			return result;
		}

		// Token: 0x06008202 RID: 33282 RVA: 0x002387C0 File Offset: 0x002369C0
		private static float GetHorizontalScaleFactor(Size imageSize, Size maxImageSize)
		{
			float result = 1f;
			if (UserPhotoUtilities.NeedsHorizontalCrop(imageSize, maxImageSize))
			{
				result = (float)maxImageSize.Width / (float)imageSize.Width;
			}
			return result;
		}

		// Token: 0x06008203 RID: 33283 RVA: 0x002387EF File Offset: 0x002369EF
		private static bool NeedsVerticalCrop(Size imageSize, Size maxImageSize)
		{
			return imageSize.Height > maxImageSize.Height;
		}

		// Token: 0x06008204 RID: 33284 RVA: 0x00238801 File Offset: 0x00236A01
		private static bool NeedsHorizontalCrop(Size imageSize, Size maxImageSize)
		{
			return imageSize.Width > maxImageSize.Width;
		}

		// Token: 0x06008205 RID: 33285 RVA: 0x00238814 File Offset: 0x00236A14
		private static Size GetImageScaledSize(Size imageSize, Size maxImageSize)
		{
			float scale = Math.Max(UserPhotoUtilities.GetVerticalScaleFactor(imageSize, maxImageSize), UserPhotoUtilities.GetHorizontalScaleFactor(imageSize, maxImageSize));
			return UserPhotoUtilities.GetScaledSize(imageSize, scale);
		}

		// Token: 0x06008206 RID: 33286 RVA: 0x0023883C File Offset: 0x00236A3C
		private static Size GetScaledSize(Size size, float scale)
		{
			int width = (int)Math.Ceiling((double)((float)size.Width * scale));
			int height = (int)Math.Ceiling((double)((float)size.Height * scale));
			return new Size(width, height);
		}

		// Token: 0x04005751 RID: 22353
		internal const int MaxFileSize = 20;

		// Token: 0x04005752 RID: 22354
		internal const int MinFileSize = 0;

		// Token: 0x04005753 RID: 22355
		internal const float LandscapeCropRatio = 0.5f;

		// Token: 0x04005754 RID: 22356
		internal const float PortraitCropRatio = 0.35f;

		// Token: 0x04005755 RID: 22357
		private const long Hash = 0L;

		// Token: 0x04005756 RID: 22358
		internal static readonly int MinImageSize = UserPhotoDimensions.HR48x48ImageSize.Height;

		// Token: 0x04005757 RID: 22359
		private static readonly Trace Tracer = ExTraceGlobals.UserPhotosTracer;
	}
}
