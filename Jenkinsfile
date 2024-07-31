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
    stage ('List') {
      steps {
        sh 'ls -la'
        sh 'pwd'
        sh 'ls blog_website -la'
      }
    }
    stage ('Migration') {
      steps {
        sh 'dotnet ef database update --project "blog_website/blog_website.csproj"'
      }
    }
    stage ('List') {
      steps {
        sh 'dotnet ef migrations list'
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
