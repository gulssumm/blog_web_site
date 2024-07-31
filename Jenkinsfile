pipeline {
  agent any
  stages {
    stage ('Initialize') {
      steps {
        echo 'GULSUM'
      }
    }
    stage ('Build') {
      steps {
        sh 'dotnet build'
      }
    }
    stage ('Test') {
      steps {
        sh 'dotnet test'
      }
    }
    stage ('List') {
      steps {
        sh 'ls -la'
        sh 'pwd'
        sh 'ls blog_website -la'
      }
    }
    stage ('Migrations-List') {
      steps {
        sh 'dotnet ef migrations list'
      }
    }
    stage ('Migration') {
      steps {
        sh 'dotnet ef database update --project "blog_website/blog_website.csproj"'
      }
    }
    stage ('Restore') {
      steps {
        sh 'dotnet restore'
      }
    }
    stage ('Run') {
      steps {
        sh 'dotnet run'
      }
    }
  }
}
