import 'package:flutter/material.dart';
import 'core/theme/app_theme.dart';

void main() {
  runApp(const DeptPickApp());
}

class DeptPickApp extends StatelessWidget {
  const DeptPickApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      debugShowCheckedModeBanner: false,
      title: 'DeptPick',
      theme: AppTheme.lightTheme,
      home: const Scaffold(body: Center(child: Text('Welcome to DeptPick'))),
    );
  }
}
