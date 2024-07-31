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
    stage ('dotnet install ef') {
      steps {
        sh 'dotnet tool install --global dotnet-ef'
        sh 'export PATH="$PATH:/var/lib/jenkins/.dotnet/tools"'
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
        sh 'dotnet-ef database update --project "blog_website/blog_website.csproj"'
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
