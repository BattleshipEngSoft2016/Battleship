var gulp = require('gulp');

var sass = require('gulp-sass');
var pug = require('gulp-pug');
var concat = require('gulp-concat');
var prettify = require('gulp-html-prettify');

gulp.task('sass', function() {
  return gulp.src('app/stylesheets/**/*.scss')
          .pipe(sass())
          .pipe(concat('all.css'))
          .pipe(gulp.dest('public/stylesheets'));
});

gulp.task('pug', function() {
  return gulp.src('app/views/*.pug')
          .pipe(pug())
          .pipe(prettify({indent_char: ' ', indent_size: 2}))
          .pipe(gulp.dest('public/views'));
});

gulp.task('scripts',function() {
  return gulp.src('app/javascripts/**/*.js')
          .pipe(concat('all.js'))
          .pipe(gulp.dest('public/javascripts'));
});

gulp.task('watch', function() {
    gulp.watch('app/javascripts/**/*.js', ['scripts']);
    gulp.watch('app/stylesheets/**/*.scss', ['sass']);
    gulp.watch('app/views/*.pug', ['pug']);
});

gulp.task('default', ['scripts', 'sass', 'pug']);
