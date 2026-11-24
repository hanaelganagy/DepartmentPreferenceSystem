import 'package:flutter/material.dart';
import 'app_colors.dart';
import 'app_text_styles.dart';

class AppTheme {
  static ThemeData lightTheme = ThemeData(
    useMaterial3: true,

    primaryColor: AppColors.primary,

    scaffoldBackgroundColor: const Color(0xFFF5F6FA),

    colorScheme: ColorScheme.fromSeed(
      seedColor: AppColors.primary,
      primary: AppColors.primary,
      secondary: AppColors.secondary,
    ),

    textTheme: const TextTheme(
      headlineLarge: AppTextStyles.heading,
      titleLarge: AppTextStyles.title,
      bodyMedium: AppTextStyles.body,
      bodySmall: AppTextStyles.caption,
    ),

    appBarTheme: const AppBarTheme(
      backgroundColor: AppColors.white,
      foregroundColor: AppColors.primary,
      elevation: 0,
      centerTitle: true,
    ),
  );
}
