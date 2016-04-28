//try{
stage 'build'
node('remote') {
		try {
		bat("C:\\WINDOWS\\Microsoft.NET\\Framework64\\v4.0.30319\\Msbuild.exe GH.NTier.Demo.sln")
		stash includes: 'GH.Northwind.UnitTest/bin/Debug/**', name: 'unit_tests'
		stash includes: 'IntegrationTests/bin/Debug/**', name: 'integration_tests'
	}
	catch(err) {
		currentBuild.result = 'FAILURE'
		//bat('git rev-parse HEAD > GIT_COMMIT')
		//git_commit=readFile('GIT_COMMIT')
		//short_commit=git_commit.take(6)
//
		//bat("C:\\Windows\\system32\\WindowsPowerShell\\v1.0\\powershell.exe -ExecutionPolicy Bypass -File d:\\Dropbox\\plex\\scripts\\prepare_email.ps1 ${short_commit} \"${env.JENKINS_HOME}\" -rootUrl ${env.JENKINS_URL} -buildUrl ${env.BUILD_URL} -ProjectName ${env.JOB_NAME} -BuildResult \"FAILURE\"")
		//$email_recepients = TrimEndOfLine(readFile("email_recepient_${short_commit}"))
		//echo $email_recepients
		////"${TrimEndOfLine(readFile("email_recepient_${short_commit}"))}
		//emailext body: "${readFile("email_template_${short_commit}")}", subject: "${env.JOB_NAME} FAILED for commit ${short_commit}", to: "pavel_pesetskiy@epam.com", replyTo: 'jenkins@plexdev.io', mimeType: 'text/html', attachLog: true
	}
}
stage 'unit tests'
parallel (
	"unit tests" : { 
		node ('master') {
			unstash 'unit_tests'
			try {
				bat("d:\\Soft\\NUnit\\bin\\nunit-console.exe .\\GH.Northwind.UnitTest\\bin\\Debug\\GH.Northwind.UnitTest.dll /xml=.\\GH.Northwind.UnitTest.xml")
			}
			catch(err) {
				echo "${err}"
				currentBuild.result = 'FAILURE'
				throw err
			}
			finally {
				step([$class: 'XUnitPublisher', testTimeMargin: '3000', thresholdMode: 1, 
				thresholds: [[$class: 'FailedThreshold', failureNewThreshold: '', failureThreshold: '', unstableNewThreshold: '', unstableThreshold: ''], 
				[$class: 'SkippedThreshold', failureNewThreshold: '', failureThreshold: '', unstableNewThreshold: '', unstableThreshold: '']], 
				tools: [[$class: 'NUnitJunitHudsonTestType', deleteOutputFiles: true, failIfNotNew: true, 
				pattern: 'GH.Northwind.UnitTest.xml', skipNoTestFiles: false, stopProcessingIfError: true]]])
			}
			
		}
	},
	"integration tests" : { 
		node ('master') {
			unstash 'integration_tests'
			try {
				bat("d:\\Soft\\NUnit\\bin\\nunit-console.exe .\\IntegrationTests\\bin\\Debug\\IntegrationTests.dll /xml=.\\IntegrationTests.xml")
			}
			catch (err){
					echo "Exception thrown:\n ${err}"
				currentBuild.result = 'FAILURE'
				throw err
			}
			finally {
				step([$class: 'XUnitPublisher', testTimeMargin: '3000', thresholdMode: 1, 
				thresholds: [[$class: 'FailedThreshold', failureNewThreshold: '', failureThreshold: '', unstableNewThreshold: '', unstableThreshold: ''], 
				[$class: 'SkippedThreshold', failureNewThreshold: '', failureThreshold: '', unstableNewThreshold: '', unstableThreshold: '']], 
				tools: [[$class: 'NUnitJunitHudsonTestType', deleteOutputFiles: true, failIfNotNew: true, 
				pattern: 'IntegrationTests.xml', skipNoTestFiles: false, stopProcessingIfError: true]]])
			}
		}
	}
)
//}
//catch (err){
//	bat("echo 'hell catch'")
//}
//
//def TrimEndOfLine($email_recepients)
//{
//	$email_recepients.replaceAll("[\n\r]", "");
//}